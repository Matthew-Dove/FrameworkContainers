using ContainerExpressions.Containers;
using System;

namespace FrameworkContainers.Format
{
    /// <summary>Access to XML serialize, and deserialize methods that return the result in a Response container.</summary>
    public sealed class XmlResponse
    {
        internal static readonly XmlResponse Instance = new XmlResponse();

        private XmlResponse() { }

        public Response<T> ToModel<T>(string xml) => ToModel<T>(xml, XmlReadOptions.Default);

        public Response<T> ToModel<T>(string xml, XmlReadOptions options)
        {
            var response = new Response<T>();

            try
            {
                var model = ExtensibleMarkupLanguage.XmlToModel<T>(xml, options);
                response = response.With(model);
            }
            catch (Exception ex)
            {
                ex.LogValue($"Error deserializing format to type {typeof(T).FullName}: {ex}");
            }

            return response;
        }

        public Response<string> FromModel<T>(T model) => FromModel(model, XmlWriteOptions.Default);

        public Response<string> FromModel<T>(T model, XmlWriteOptions options)
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

    /// <summary>Access to XML serialize, and deserialize methods that return the result in a Response container (for a single type).</summary>
    public sealed class XmlResponse<T>
    {
        internal static readonly XmlResponse<T> Instance = new XmlResponse<T>();

        private XmlResponse() { }

        public Response<T> ToModel(string xml) => XmlResponse.Instance.ToModel<T>(xml);

        public Response<T> ToModel(string xml, XmlReadOptions options) => XmlResponse.Instance.ToModel<T>(xml, options);

        public Response<string> FromModel(T model) => XmlResponse.Instance.FromModel<T>(model);

        public Response<string> FromModel(T model, XmlWriteOptions options) => XmlResponse.Instance.FromModel<T>(model, options);
    }
}
