namespace FrameworkContainers.Models
{
    internal static class Constants
    {
        public static class Format
        {
            public const string DESERIALIZE_ERROR_MESSAGE = "Error deserializing format to model.";
            public const string SERIALIZE_ERROR_MESSAGE = "Error serializing model to format.";
        }

        public static class Serialize
        {
            public const int MAX_DEPTH = 32; // Max nodes to read before giving up.
            public const int MAX_READ_LENGTH = 16384; // The maximum string length returned by the reader.
        }

        public static class Http
        {
            public const string JSON_CONTENT = "application/json"; // An alterative would be: "application/json; charset=utf-8", but UTF-8 is already the default encoding for JSON (making it redundant).

            public const string GET = "GET";
            public const string POST = "POST";
            public const string PUT = "PUT";
            public const string PATCH = "PATCH";
            public const string DELETE = "DELETE";

            public const int TIMEOUT_SECONDS = 15;

            public const int DEFAULT_HTTP_CODE = 504;
            public const string DEFAULT_HTTP_DESCRIPTION = "GatewayTimeout";
        }
    }
}
