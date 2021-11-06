using ContainerExpressions.Containers;
using System;

namespace FrameworkContainers.Format
{
    /// <summary>Access to XML serialize, and deserialize methods that return the result in a Response container.</summary>
    public sealed class XmlResponse
    {
        internal XmlResponse() { }

        public Response<T> ToModel<T>(string xml)
        {
            var response = new Response<T>();

            try
            {
                var model = ExtensibleMarkupLanguage.XmlToModel<T>(xml);
                response = response.With(model);
            }
            catch (Exception ex)
            {
                ex.LogValue($"Error deserializing format to type {typeof(T).FullName}: {ex}");
            }

            return response;
        }

        public Response<string> FromModel<T>(T model) => FromModel(model, XmlOptions.Default);

        public Response<string> FromModel<T>(T model, XmlOptions options)
        {
            var response = new Response<string>();

            try
            {
                var json = ExtensibleMarkupLanguage.ModelToXml<T>(model, options);
                response = response.With(json);
            }
            catch (Exception ex)
            {
                ex.LogValue($"Error deserializing format to type {typeof(T).FullName}: {ex}");
            }

            return response;
        }
    }
}
