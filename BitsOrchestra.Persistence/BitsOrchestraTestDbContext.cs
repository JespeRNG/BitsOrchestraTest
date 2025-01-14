using System.Data.Entity;
using BitsOrchestraTest.Domains.Contacts;
using BitsOrchestra.Persistence.Configurations;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace BitsOrchestraTest.Persistence
{
    public class BitsOrchestraTestDbContext : DbContext
    {
        public BitsOrchestraTestDbContext() : base("BitsOrchestraTestDb")
        {
        }

        public BitsOrchestraTestDbContext(string nameOrConnectionString) : base(nameOrConnectionString)
        { }

        public DbSet<Contact> Contacts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ContactConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        { 
            var entries = ChangeTracker.Entries()
                .Where(e => e.Entity is Contact && e.State == EntityState.Added);
            
            foreach (var entry in entries) 
            { 
                var entity = (Contact)entry.Entity; 
                entity.Id = Guid.NewGuid();
            } 
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
