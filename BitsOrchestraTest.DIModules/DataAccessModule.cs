using Autofac;
using Mehdime.Entity;
using System.Data.Entity;
using BitsOrchestra.Persistence;
using BitsOrchestraTest.Persistence;
using BitsOrchestraTest.Persistence.Interfaces;

namespace BitsOrchestraTest.DIModules
{
    public class DataAccessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<BitsOrchestraTestDbContext>().As<DbContext>().InstancePerLifetimeScope();

            builder.RegisterType<BitsOrchestraRepository>().As<IRepository>();

            builder.RegisterType<AmbientDbContextLocator>().As<IAmbientDbContextLocator>();

            builder.RegisterType<DbContextScopeFactory>().As<IDbContextScopeFactory>();
        }
    }
}
