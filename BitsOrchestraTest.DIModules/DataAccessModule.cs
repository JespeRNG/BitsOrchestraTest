using Autofac;
using Mehdime.Entity;
using BitsOrchestra.Persistence;
using BitsOrchestraTest.Persistence.Interfaces;
using BitsOrchestraTest.Persistence;
using System.Data.Entity;

namespace BitsOrchestraTest.DIModules
{
    public class DataAccessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            /*builder.RegisterType(typeof(AmbientDbContextLocator))
                .As(typeof(IAmbientDbContextLocator));

            builder.RegisterType(typeof(DbContextScopeFactory))
                .As(typeof(IDbContextScopeFactory));

            builder.RegisterType(typeof(BitsOrchestraRepository))
                .As(typeof(IRepository));*/

            builder.RegisterType<BitsOrchestraTestDbContext>().As<DbContext>().InstancePerLifetimeScope();

            builder.RegisterType<BitsOrchestraRepository>().As<IRepository>();
            

            builder.RegisterType<AmbientDbContextLocator>().As<IAmbientDbContextLocator>();
            builder.RegisterType<DbContextScopeFactory>().As<IDbContextScopeFactory>();
        }
    }
}
