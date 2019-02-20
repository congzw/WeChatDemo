using System;
using System.Web.Mvc;
using StructureMap.Configuration.DSL;
using StructureMap.Graph;
using StructureMap.Pipeline;
using StructureMap.TypeRules;

namespace MvcApp.Web.Dependencies.DependencyResolution {

    public class ControllerConvention : IRegistrationConvention 
    {
        public void Process(Type type, Registry registry)
        {
            if (type.CanBeCastTo<Controller>() && !type.IsAbstract)
            {
                //MVC: System.Web.Mvc.ControllerBase.VerifyExecuteCalledOnce()
                //A single instance of controller 'XxxController' cannot be used to handle multiple requests. 
                //If a custom controller factory is in use, make sure that it creates a new instance of the controller for each request.
                registry.For(type).LifecycleIs(new UniquePerRequestLifecycle());
            }
        }
    }
}