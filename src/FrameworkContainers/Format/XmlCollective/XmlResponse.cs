using ContainerExpressions.Containers;
using System;

namespace FrameworkContainers.Format.XmlCollective
{
    /// <summary>Access to XML serialize, and deserialize methods that return the result in a Response container.</summary>
    public sealed class XmlResponse
    {
        internal static readonly XmlResponse Instance = new XmlResponse();

        private XmlResponse() { }

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

        public Response<string> FromModel<T>(T model)
        {
            var response = new Response<string>();

            try
            {
                var json = ExtensibleMarkupLanguage.ModelToXml<T>(model);
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

        public Response<string> FromModel(T model) => XmlResponse.Instance.FromModel<T>(model);
    }
}
