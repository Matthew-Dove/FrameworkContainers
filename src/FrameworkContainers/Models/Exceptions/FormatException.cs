using System;

namespace FrameworkContainers.Models.Exceptions
{
    public enum FormatRange { Json, Xml }

    public class FormatDeserializeException : Exception
    {
        public FormatRange Format { get; }
        public Type TargetType { get; }
        public string Input { get; }

        public FormatDeserializeException(string message, Exception innerException, FormatRange format, Type targetType, string input) : base(message, innerException)
        {
            Format = format;
            TargetType = targetType;
            Input = input;
        }
    }

    public class FormatSerializeException : Exception
    {
        public FormatRange Format { get; }
        public object Model { get; }

        public FormatSerializeException(string message, Exception innerException, FormatRange format, object model) : base(message, innerException)
        {
            Format = format;
            Model = model;
        }
    }
}
