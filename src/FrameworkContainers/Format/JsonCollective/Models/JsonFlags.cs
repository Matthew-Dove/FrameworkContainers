using System;

namespace FrameworkContainers.Format.JsonCollective.Models
{
    [Flags]
    public enum JsonFlags
    {
        /// <summary>The default behaviour: Performant | CamelCase.</summary>
        Default = 0,

        /// <summary>Optimized for the best serializer performance.</summary>
        Performant = 1,

        /// <summary>Optimized to work in a wide range of cases.</summary>
        Permissive = 2,

        /// <summary>JSON naming policy is camelCase.</summary>
        CamelCase = 4,

        /// <summary>Json naming policy follows the model's casing policy.</summary>
        ModelCase = 8
    }
}
