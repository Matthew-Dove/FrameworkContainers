using System;

namespace FrameworkContainers.Format.JsonCollective.Models
{
    [Flags]
    public enum JsonFlags
    {
        Default = 0,
        Performant = 1,
        PerformantCamelCase = 2,
        Permissive = 4,
        PermissiveCamelCase = 8
    }
}
