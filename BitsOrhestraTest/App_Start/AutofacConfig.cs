using Autofac;
using System.Web.Mvc;
using System.Reflection;
using Autofac.Integration.Mvc;
using BitsOrchestraTest.DIModules;

namespace BitsOrhestraTest.App_Start
{
    public static class AutofacConfig
    {
        public static void RegisterDependencies()
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterModule(new BusinessLogicModule());
            builder.RegisterModule(new DataAccessModule());

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}