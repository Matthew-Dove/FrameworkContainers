namespace FrameworkContainers.Common
{
    /// <summary>A place to put shared common functions.</summary>
    internal sealed class FuncPool
    {
        /// <summary>Maps the input directly to the output.</summary>
        public static T Identity<T>(T x) => x;
    }
}
