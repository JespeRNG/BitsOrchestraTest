using System;
using Mehdime.Entity;
using System.Data.Entity;
using BitsOrchestraTest.Persistence;

namespace BitsOrchestra.Persistence
{
    public class BitsOrchestraRepository : Repository
    {
        private readonly IAmbientDbContextLocator _dbContextLocator;

        public BitsOrchestraRepository(IAmbientDbContextLocator contextLocator)
        {
            _dbContextLocator = contextLocator ?? throw new ArgumentNullException(nameof(contextLocator));
        }

        protected override DbContext DbContext
        {
            get
            {
                var dbContext = _dbContextLocator.Get<BitsOrchestraTestDbContext>();
                if (dbContext == null)
                {
                    throw new InvalidOperationException("No ambient DbContext of type BitsOrchestraTestDbContext found. Make sure that repository is used in the DbContextScope.");
                }
                return dbContext;
            }
        }
    }
}
