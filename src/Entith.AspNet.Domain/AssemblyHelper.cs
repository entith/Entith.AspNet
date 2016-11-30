using Microsoft.Extensions.DependencyModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Entith.AspNet.Domain
{
    // http://www.michael-whelan.net/replacing-appdomain-in-dotnet-core/
    public static class AssemblyHelper
    {
        public static IEnumerable<Type> GetAssemblies()
        {

#if NET451
            return AppDomain.CurrentDomain.GetAssemblies().SelectMany(a => a.GetTypes());
#else
            var assemblies = new List<Assembly>();
            var dependencies = DependencyContext.Default.RuntimeLibraries;
            foreach (var library in dependencies)
            {
                try
                {
                    var assembly = Assembly.Load(new AssemblyName(library.Name));
                    assemblies.Add(assembly);
                }
                catch
                {

                }
            }
            return assemblies.SelectMany(a => a.ExportedTypes);
#endif
        }
    }
}
