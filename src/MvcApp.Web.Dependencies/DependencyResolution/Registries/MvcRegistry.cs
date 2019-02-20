using System;
using CommonFx.Common;
using StructureMap.Configuration.DSL;
using StructureMap.Graph;

namespace MvcApp.Web.Dependencies.DependencyResolution.Registries
{
    public class MvcRegistry : Registry
    {
        public MvcRegistry()
        {
            var projectPrefix = MyProjectHelper.Resolve().GetProjectPrefix();

            Scan(
                scan =>
                {
                    //assemblies
                    scan.AssembliesFromApplicationBaseDirectory(assembly =>
                    {
                        var name = assembly.GetName().Name;
                        return name.StartsWith(projectPrefix) || name.StartsWith("CommonFx");;
                    });

                    //scan.TheCallingAssembly();
                    scan.WithDefaultConventions();
                    scan.With(new ControllerConvention());
                });
        }
    }
}