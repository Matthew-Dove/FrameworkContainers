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
            public const int MAX_DEPTH = 32;
        }
    }
}
