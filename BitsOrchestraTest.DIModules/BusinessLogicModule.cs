using Autofac;
using BitsOrchestraTest.Services;
using BitsOrchestraTest.Core.Interfaces;

namespace BitsOrchestraTest.DIModules
{
    public class BusinessLogicModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ContactService>().As<IContactService>();
        }
    }
}
