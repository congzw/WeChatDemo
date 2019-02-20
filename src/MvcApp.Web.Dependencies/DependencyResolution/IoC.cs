using System.Diagnostics;
using CommonFx.Common;
using CommonServiceLocator;
using CommonFx.Common.Ioc;
using StructureMap;
using StructureMap.Graph;

namespace MvcApp.Web.Dependencies.DependencyResolution
{
    public static class IoC
    {
        public static IContainer Initialize()
        {
            var projectPrefix = MyProjectHelper.Resolve().GetProjectPrefix();
            var container = new Container(cfg =>
            {
                cfg.Scan(scanner =>
                {
                    //assemblies
                    scanner.AssembliesFromApplicationBaseDirectory(assembly =>
                    {
                        var name = assembly.GetName().Name;
                        return name.StartsWith(projectPrefix) || name.StartsWith("CommonFx");
                    });

                    //registries
                    scanner.LookForRegistries();
                });

                //global register
                cfg.For<IServiceLocator>().Use<StructureMapDependencyScope>().Singleton();
            });

            CoreServiceProvider.CurrentFunc = () => container.GetInstance<IServiceLocator>();

            ShowDebugInfo(container);

            return container;
        }

        private static void ShowDebugInfo(Container container)
        {
            //var whatDidIScan = container.WhatDidIScan();
            //Debug.Write(whatDidIScan);

            var whatDoIHave = container.WhatDoIHave();
            Debug.Write(whatDoIHave);
        }
    }
}