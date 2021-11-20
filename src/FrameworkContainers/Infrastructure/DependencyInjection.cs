using System;
using System.Linq;
using System.Reflection;

namespace FrameworkContainers.Infrastructure
{
    /// <summary>Adds types to your dependency injection framework.</summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// Matches IService to Service.
        /// <para>In sandbox mode, matches IService to ServiceSandbox, or ServiceStandalone.</para>
        /// <para>Example usage with ASP.NET's IServiceCollection: DependencyInjection.AddServicesByConvention((x, y) => services.AddSingleton(x, y));.</para>
        /// </summary>
        /// <param name="isSandbox">When in sandbox mode, check if a sandbox implementation exists, and prefer that version.</param>
        /// <param name="assemblyStartsWith">Adds any referenced assemblies starting with this name.</param>
        /// <param name="explicitlyNamedAssemblies">Add any assemblies that may not be loaded yet, but you'd like included.</param>
        /// <param name="resolver">A wrapper around the dependency injection framework you're using.</param>
        public static void AddServicesByConvention(Action<Type, Type> resolver, bool isSandbox = false, string assemblyStartsWith = null, params string[] explicitlyNamedAssemblies)
        {
            var types = GetTypesFromAssemblies(assemblyStartsWith, explicitlyNamedAssemblies);
            AddServicesByConvention(types, resolver, isSandbox);
        }

        /// <summary>Scans all assemblies supplied, extracting the exported types.</summary>
        private static Type[] GetTypesFromAssemblies(string assemblyStartsWith = null, params string[] explicitlyNamedAssemblies)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var explicitAssemblies = explicitlyNamedAssemblies.Select(x => Assembly.Load(x));
            var referencedAssemblies = Enumerable.Empty<Assembly>();
            var explicitReferencedAssemblies = Enumerable.Empty<Assembly>();
            if (!string.IsNullOrEmpty(assemblyStartsWith))
            {
                referencedAssemblies = assembly.GetReferencedAssemblies().Where(x => x.Name.StartsWith(assemblyStartsWith)).Select(y => Assembly.Load(y));
                explicitReferencedAssemblies = explicitAssemblies.SelectMany(x => x.GetReferencedAssemblies().Where(y => y.Name.StartsWith(assemblyStartsWith)).Select(z => Assembly.Load(z)));
            }
            var assemblies = new Assembly[] { assembly }.Concat(referencedAssemblies).Concat(explicitAssemblies).Concat(explicitReferencedAssemblies).Distinct();
            return assemblies.SelectMany(x => x.GetExportedTypes()).ToArray();
        }

        /// <summary>
        /// Match IService to Service, where IService is an interface and Service is a class.
        /// <para>When in sandbox mode, check if a sandbox implementation exists, and prefer that version.</para>
        /// </summary>
        private static void AddServicesByConvention(Type[] types, Action<Type, Type> resolver, bool isSandbox)
        {
            foreach (Type @interface in types.Where(y => y.IsInterface))
            {
                var match = @interface.Name.Substring(1);
                var implementation = types.FirstOrDefault(x => x.Name == match && x.IsClass);

                if (isSandbox && implementation != default)
                {
                    var sandboxImplementation = types.FirstOrDefault(x => (x.Name == match + "Sandbox" || x.Name == match + "Standalone") && x.IsClass);
                    if (sandboxImplementation != default)
                    {
                        implementation = sandboxImplementation;
                    }
                }

                if (implementation != default && implementation.GetInterfaces().Contains(@interface))
                {
                    resolver(@interface, implementation);
                }
            }
        }
    }
}
