using FrameworkContainers.Models.Exceptions;
using FrameworkContainers.Models;
using System;
using System.Text.Json;

namespace FrameworkContainers.Common
{
    internal static class ThrowHelper
    {
        #region Error

        public static void JsonSerializeError(Exception ex, object model)
        {
            throw new FormatSerializeException(Constants.Format.SERIALIZE_ERROR_MESSAGE, ex, FormatRange.Json, model);
        }

        public static void JsonDeserializeError(Exception ex, Type targetType, string input)
        {
            throw new FormatDeserializeException(Constants.Format.DESERIALIZE_ERROR_MESSAGE, ex, FormatRange.Json, targetType, input);
        }

        public static void XmlSerializeError(Exception ex, object model)
        {
            throw new FormatSerializeException(Constants.Format.SERIALIZE_ERROR_MESSAGE, ex, FormatRange.Xml, model);
        }

        public static void XmlDeserializeError(Exception ex, Type targetType, string input)
        {
            throw new FormatDeserializeException(Constants.Format.DESERIALIZE_ERROR_MESSAGE, ex, FormatRange.Xml, targetType, input);
        }

        public static void JsonConverterError(string message)
        {
            throw new JsonException(message);
        }

        #endregion

        #region Exception

        public static void ArgumentOutOfRangeException(string name) => throw new ArgumentOutOfRangeException(name);

        #endregion
    }
}
