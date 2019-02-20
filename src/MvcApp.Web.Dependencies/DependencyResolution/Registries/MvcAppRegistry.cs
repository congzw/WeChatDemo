using CommonFx.Common.Security;
using MvcApp.Web._Impls;
using StructureMap.Configuration.DSL;

namespace MvcApp.Web.DependencyResolution.Registries
{
    public class AuthsRegistry : Registry
    {
        public AuthsRegistry()
        {
            For<IAuthManager>().Use<FormAuthManager>();
        }
    }
}
