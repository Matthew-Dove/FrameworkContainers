using FrameworkContainers.Log;

namespace FrameworkContainers
{
    /// <summary>
    /// Entry point to access all framework containers.
    /// <para>Each container captures some useful part of a framework, making it easy to use.</para>
    /// </summary>
    public static class FrameworkContainer
    {
        /// <summary>Access to logging related Containers.</summary>
        public static LogContainer Log { get; } = new LogContainer();
    }
}
