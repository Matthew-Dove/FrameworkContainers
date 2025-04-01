using FrameworkContainers.Format.JsonCollective;
using FrameworkContainers.Format.XmlCollective;
using FrameworkContainers.Network.HttpCollective;
using FrameworkContainers.Network.SqlCollective;
using System;
using System.Linq;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>Adds types to your dependency injection framework.</summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// Matches IService to Service.
        /// <para>In sandbox mode, matches IService to ServiceSandbox.</para>
        /// <para>Example usage with ASP.NET's IServiceCollection: builder.services.AddServicesByConvention();.</para>
        /// </summary>
        /// <param name="services">The service collection used for dependency injection.</param>
        /// <param name="assembly">The caller's main assembly i.e. Assembly.GetExecutingAssembly().GetName().FullName;.</param>
        /// <param name="isSandbox">When in sandbox mode, check if a sandbox implementation exists, and prefer that version.</param>
        /// <param name="scanInternals">Includes internal classes as targets for dependency injection matches.</param>
        /// <param name="assemblyStartsWith">Adds any referenced assemblies starting with this name.</param>
        /// <param name="explicitlyNamedAssemblies">Add any assemblies that may not be loaded yet, but you'd like included.</param>
        /// <returns></returns>
        public static IServiceCollection AddServicesByConvention(this IServiceCollection services, string assembly, bool isSandbox = false, bool scanInternals = false, string assemblyStartsWith = null, params string[] explicitlyNamedAssemblies)
        {
            AddServicesByConvention((x, y) => services.AddSingleton(x, y), assembly, isSandbox, scanInternals, assemblyStartsWith, explicitlyNamedAssemblies);
            return services;
        }

        /// <summary>
        /// Matches IService to Service.
        /// <para>In sandbox mode, matches IService to ServiceSandbox.</para>
        /// <para>Example usage with ASP.NET's IServiceCollection: DependencyInjection.AddServicesByConvention((x, y) => services.AddSingleton(x, y));.</para>
        /// </summary>
        /// <param name="resolver">A wrapper around the dependency injection framework you're using.</param>
        /// <param name="assembly">The caller's main assembly i.e. Assembly.GetExecutingAssembly().GetName().FullName;.</param>
        /// <param name="isSandbox">When in sandbox mode, check if a sandbox implementation exists, and prefer that version.</param>
        /// <param name="scanInternals">Includes internal classes as targets for dependency injection matches.</param>
        /// <param name="assemblyStartsWith">Adds any referenced assemblies starting with this name.</param>
        /// <param name="explicitlyNamedAssemblies">Add any assemblies that may not be loaded yet, but you'd like included.</param>
        public static void AddServicesByConvention(Action<Type, Type> resolver, string assembly, bool isSandbox = false, bool scanInternals = false, string assemblyStartsWith = null, params string[] explicitlyNamedAssemblies)
        {
            var types = GetTypesFromAssemblies(assembly, scanInternals, assemblyStartsWith, explicitlyNamedAssemblies);
            AddServicesByConvention(types, resolver, isSandbox);
        }

        /// <summary>Scans all assemblies supplied, extracting the exported types.</summary>
        private static Type[] GetTypesFromAssemblies(string assembly, bool scanInternals = false, string assemblyStartsWith = null, params string[] explicitlyNamedAssemblies)
        {
            var mainAssembly = Assembly.Load(assembly);
            var explicitAssemblies = explicitlyNamedAssemblies.Select(Assembly.Load);

            var referencedAssemblies = Enumerable.Empty<Assembly>();
            var explicitReferencedAssemblies = Enumerable.Empty<Assembly>();
            if (!string.IsNullOrEmpty(assemblyStartsWith))
            {
                referencedAssemblies = mainAssembly.GetReferencedAssemblies().Where(x => x.Name.StartsWith(assemblyStartsWith)).Select(Assembly.Load);
                explicitReferencedAssemblies = explicitAssemblies.SelectMany(x => x.GetReferencedAssemblies().Where(y => y.Name.StartsWith(assemblyStartsWith)).Select(Assembly.Load));
            }

            var assemblies = new Assembly[] { mainAssembly }.Concat(referencedAssemblies).Concat(explicitAssemblies).Concat(explicitReferencedAssemblies).Distinct();
            try
            {
                if (scanInternals) return assemblies.SelectMany(x => x.GetTypes()).ToArray();
                return assemblies.SelectMany(x => x.GetExportedTypes()).ToArray();
            }
            catch (ReflectionTypeLoadException ex)
            {
                return ex.Types.Where(x => x != null).ToArray();
            }
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
                    var sandboxImplementation = types.FirstOrDefault(x => x.Name == match + "Sandbox" && x.IsClass);
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

        /// <summary>Adds all of framework's client interfaces, and implementations to DI.</summary>
        public static IServiceCollection AddFrameworkEverythingEverywhereAllAtOnce(this IServiceCollection services)
        {
            AddFrameworkEverythingEverywhereAllAtOnce((x, y) => services.AddSingleton(x, y));
            return services;
        }

        /// <summary>Adds all of framework's client interfaces, and implementations to DI.</summary>
        public static void AddFrameworkEverythingEverywhereAllAtOnce(Action<Type, Type> resolver)
        {
            AddFrameworkXmlClient(resolver);
            AddFrameworkJsonClient(resolver);
            AddFrameworkSqlClient(resolver);
            AddFrameworkHttpClient(resolver);
        }

        /// <summary>Adds XmlClient's interfaces, and implementations to DI.</summary>
        public static IServiceCollection AddFrameworkXmlClient(this IServiceCollection services)
        {
            AddFrameworkXmlClient((x, y) => services.AddSingleton(x, y));
            return services;
        }

        /// <summary>Adds XmlClient's interfaces, and implementations to DI.</summary>
        public static void AddFrameworkXmlClient(Action<Type, Type> resolver)
        {
            resolver(typeof(IXmlClient), typeof(XmlClient));
            resolver(typeof(IXmlClient<>), typeof(XmlClient<>));
        }

        /// <summary>Adds JsonClient's interfaces, and implementations to DI.</summary>
        public static IServiceCollection AddFrameworkJsonClient(this IServiceCollection services)
        {
            AddFrameworkJsonClient((x, y) => services.AddSingleton(x, y));
            return services;
        }

        /// <summary>Adds JsonClient's interfaces, and implementations to DI.</summary>
        public static void AddFrameworkJsonClient(Action<Type, Type> resolver)
        {
            resolver(typeof(IJsonClient), typeof(JsonClient));
            resolver(typeof(IJsonClient<>), typeof(JsonClient<>));
        }

        /// <summary>Adds SqlClient's interfaces, and implementations to DI.</summary>
        public static IServiceCollection AddFrameworkSqlClient(this IServiceCollection services)
        {
            AddFrameworkSqlClient((x, y) => services.AddSingleton(x, y));
            return services;
        }

        /// <summary>Adds SqlClient's interfaces, and implementations to DI.</summary>
        public static void AddFrameworkSqlClient(Action<Type, Type> resolver)
        {
            resolver(typeof(ISqlClient), typeof(SqlClient));
            resolver(typeof(ISqlClient<>), typeof(SqlClient<>));
        }

        /// <summary>Adds HttpClient's interfaces, and implementations to DI.</summary>
        public static IServiceCollection AddFrameworkHttpClient(this IServiceCollection services)
        {
            AddFrameworkHttpClient((x, y) => services.AddSingleton(x, y));
            return services;
        }

        /// <summary>Adds HttpClient's interfaces, and implementations to DI.</summary>
        public static void AddFrameworkHttpClient(Action<Type, Type> resolver)
        {
            resolver(typeof(IHttpClient), typeof(HttpClient));
            resolver(typeof(IHttpClient<>), typeof(HttpClient<>));
            resolver(typeof(IHttpClient<,>), typeof(HttpClient<,>));
        }
    }
}
