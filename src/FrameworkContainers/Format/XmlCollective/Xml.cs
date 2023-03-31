using FrameworkContainers.Format.XmlCollective.Models;
using FrameworkContainers.Models;
using FrameworkContainers.Models.Exceptions;
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

        public static T ToModel<T>(string xml) => ToModel<T>(xml, XmlReadOptions.Default);

        public static T ToModel<T>(string xml, XmlReadOptions options)
        {
            try
            {
                return ExtensibleMarkupLanguage.XmlToModel<T>(xml, options);
            }
            catch (Exception ex)
            {
                DeserializeError(ex, typeof(T), xml);
            }
            return default;
        }

        public static string FromModel<T>(T model) => FromModel(model, XmlWriteOptions.Default);

        public static string FromModel<T>(T model, XmlWriteOptions options)
        {
            try
            {
                return ExtensibleMarkupLanguage.ModelToXml<T>(model, options);
            }
            catch (Exception ex)
            {
                SerializeError(ex, model);
            }
            return default;
        }

        private static void SerializeError(Exception ex, object model)
        {
            throw new FormatSerializeException(Constants.Format.SERIALIZE_ERROR_MESSAGE, ex, FormatRange.Xml, model);
        }

        private static void DeserializeError(Exception ex, Type targetType, string input)
        {
            throw new FormatDeserializeException(Constants.Format.DESERIALIZE_ERROR_MESSAGE, ex, FormatRange.Xml, targetType, input);
        }
    }
}
