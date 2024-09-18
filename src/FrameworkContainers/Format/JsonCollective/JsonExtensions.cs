using ContainerExpressions.Containers;
using FrameworkContainers.Format.JsonCollective.Models;
using FrameworkContainers.Format.JsonCollective.Models.Converters;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace FrameworkContainers.Format.JsonCollective
{
    public static class JsonExtensions
    {
        public static void AddJsonConverters(this JsonSerializerOptions options)
        {
            var converters = JsonOptions.GetJsonConverters();
            foreach (var converter in converters)
            {
                if (!options.Converters.Contains(converter))
                {
                    options.Converters.Add(converter);
                }
            }
        }

        public static void AddEnumConverter<TEnum>(this IList<JsonConverter> converters) where TEnum : struct
        {
            var converter = new DefaultEnumConverter<TEnum>();
            if (!converters.Contains(converter))
            {
                converters.Add(converter);
            }
        }

        public static void AddSmartEnumConverter<TSmartEnum>(this IList<JsonConverter> converters) where TSmartEnum : SmartEnum
        {
            var converter = new SmartEnumConverter<TSmartEnum>();
            if (!converters.Contains(converter))
            {
                converters.Add(converter);
            }
        }
    }
}
