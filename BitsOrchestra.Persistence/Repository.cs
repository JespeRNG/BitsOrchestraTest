using System.Linq;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Data.Entity.Infrastructure;
using BitsOrchestraTest.Persistence.Interfaces;

namespace BitsOrchestraTest.Persistence
{
    public abstract class Repository : IRepository
    {
        public IQueryable<T> GetAll<T>() where T : class
        {
            return DbContext.Set<T>();
        }

        public async Task<bool> IsExistAsync<T, TKey>(TKey id) where T : class
        {
            return await DbContext.Set<T>().FindAsync(id) != null;
        }

        public T Find<T>(int id) where T : class
        {
            return DbContext.Set<T>().Find(id);
        }

        public async Task<T> FindAsync<T>(int id) where T : class
        {
            return await DbContext.Set<T>().FindAsync(id);
        }

        public async Task<T> FindAsync<T, TKey>(TKey id) where T : class
        {
            return await DbContext.Set<T>().FindAsync(id);
        }

        public T Add<T>(T entity) where T : class
        {
            return DbContext.Set<T>().Add(entity);
        }

        public IEnumerable<T> AddRange<T>(IEnumerable<T> entities) where T : class
        {
            return DbContext.Set<T>().AddRange(entities);
        }

        public void Remove<T>(T entity) where T : class
        {
            DbContext.Set<T>().Remove(entity);
        }

        public void RemoveRange<T>(IEnumerable<T> entities) where T : class
        {
            DbContext.Set<T>().RemoveRange(entities);
        }

        public void Update<T>(T entity) where T : class
        {
            DbContext.Entry(entity).State = EntityState.Modified;
        }

        public void UpdateRange<T>(IEnumerable<T> entities) where T : class
        {
            foreach (var entity in entities)
            {
                DbContext.Entry(entity).State = EntityState.Modified;
            }
        }

        public void Upsert<T>(T entity) where T : class
        {
            DbContext.Set<T>().AddOrUpdate(entity);
        }

        public DbRawSqlQuery<T> SqlQuery<T>(string sql, params object[] parameters) where T : class
        {
            return DbContext.Database.SqlQuery<T>(sql, parameters);
        }

        public virtual async Task<int> ExecuteSqlCommandAsync(TransactionalBehavior transactionalBehavior, string sql, params object[] parameters)
        {
            return await DbContext.Database.ExecuteSqlCommandAsync(transactionalBehavior, sql, parameters);
        }

        public virtual async Task<int> ExecuteSqlCommandAsync(string sql, params object[] parameters)
        {
            return await DbContext.Database.ExecuteSqlCommandAsync(sql, parameters);
        }

        protected abstract DbContext DbContext { get; }
    }
}
