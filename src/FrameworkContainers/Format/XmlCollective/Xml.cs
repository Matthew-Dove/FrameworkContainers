using System;

namespace FrameworkContainers.Format.XmlCollective
{
    /// <summary>Serialize, and deserialize between string, and model.</summary>
    public static class Xml
    {
        /// <summary>Access to XML serialize, and deserialize methods that return the result in a Maybe container.</summary>
        public static readonly XmlMaybe Maybe = XmlMaybe.Instance;

        /// <summary>Access to XML serialize, and deserialize methods that return the result in a Response container.</summary>
        public static readonly XmlResponse Response = XmlResponse.Instance;

        public static T ToModel<T>(string xml)
        {
            try
            {
                return ExtensibleMarkupLanguage.XmlToModel<T>(xml);
            }
            catch (Exception ex)
            {
                XmlDeserializeError(ex, typeof(T), xml);
            }
            return default;
        }

        public static string FromModel<T>(T model)
        {
            try
            {
                return ExtensibleMarkupLanguage.ModelToXml<T>(model);
            }
            catch (Exception ex)
            {
                XmlSerializeError(ex, model);
            }
            return default;
        }
    }

    public static class Xml<T>
    {
        public static readonly XmlClient<T> Client = new XmlClient<T>();
    }
}
