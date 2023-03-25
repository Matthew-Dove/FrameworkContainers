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
    }
}
