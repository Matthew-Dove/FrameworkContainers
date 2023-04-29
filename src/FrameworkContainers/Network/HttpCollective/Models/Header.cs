using System.Runtime.CompilerServices;

namespace FrameworkContainers.Network.HttpCollective.Models
{
    public readonly struct Header
    {
        public string Key { get; }
        public string Value { get; }

        public Header(string key, string value)
        {
            Key = key.ThrowIfNullOrEmpty();
            Value = value.ThrowIfNullOrEmpty();
        }
    }

    file static class HeaderExtensions
    {
        public static string ThrowIfNullOrEmpty(
            this string target,
            [CallerArgumentExpression(nameof(target))] string argument = "",
            [CallerMemberName] string caller = "",
            [CallerFilePath] string path = "",
            [CallerLineNumber] int line = 0
            )
        {
            if (string.IsNullOrEmpty(target)) ArgumentOutOfRangeException(argument);
            return target;
        }

        // Min, Max, collectionIsNullOrEmpty, ifNull, ifDefault.
        // ThrowIfLessThan, ThrowIfGreaterThen, ThrowIfNullOrEmpty, ThrowIfNull, ThrowIfDefault.
        // where T : struct, IComparable<T>
    }
}

#if !NETCOREAPP3_0_OR_GREATER
namespace System.Runtime.CompilerServices
{
    /// <summary>
    /// Contains the source code passed in as a method's parameter, this is determined at compile time.
    /// <para>For example this could be a variable name (foo), an expression (foo > bar), or some object creation (new { foo = bar}).</para>
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = false)]
    internal sealed class CallerArgumentExpressionAttribute : Attribute
    {
        public string ParameterName { get; }

        public CallerArgumentExpressionAttribute(string parameterName)
        {
            ParameterName = parameterName;
        }
    }
}
#endif
