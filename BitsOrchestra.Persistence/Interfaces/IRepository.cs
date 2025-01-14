using System.Linq;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;

namespace BitsOrchestraTest.Persistence.Interfaces
{
    public interface IRepository
    {
        /// <summary>
        ///     Returns queryable collection with all entities.
        /// </summary>
        IQueryable<T> GetAll<T>() where T : class;

        /// <summary>
        ///     Check is entity exist
        /// </summary>
        /// <param name="id">Id of the entity.</param>
        /// <returns>true if exist.</returns>
        Task<bool> IsExistAsync<T, TKey>(TKey id) where T : class;

        /// <summary>
        ///     Returns entity by id.
        /// </summary>
        /// <param name="id">Id of the entity.</param>
        T Find<T>(int id) where T : class;

        /// <summary>
        ///     Asynchronously returns entity by id.
        /// </summary>
        /// <param name="id">Id of the entity.</param>
        Task<T> FindAsync<T>(int id) where T : class;

        /// <summary>
        ///     Asynchronously returns entity by generic Tkey.
        /// </summary>
        /// <param name="id">Id of the entity.</param>
        Task<T> FindAsync<T, TKey>(TKey id) where T : class;

        /// <summary>
        ///     Adds new entity.
        /// </summary>
        /// <param name="entity">Entity that should be created.</param>
        /// <returns>Returns new created entity.</returns>
        T Add<T>(T entity) where T : class;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entityes"></param>
        /// <returns></returns>
        IEnumerable<T> AddRange<T>(IEnumerable<T> entityes) where T : class;

        /// <summary>
        ///     Removes entity.
        /// </summary>
        /// <param name="entity">Entity that should be removed.</param>
        void Remove<T>(T entity) where T : class;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entities"></param>
        void RemoveRange<T>(IEnumerable<T> entities) where T : class;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        void Update<T>(T entity) where T : class;

        /// <summary>
        ///     Updates entities
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entities"></param>
        void UpdateRange<T>(IEnumerable<T> entities) where T : class;

        /// <summary>
        ///     Equivalent to an Upsert, select and insert or update if already exist
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        void Upsert<T>(T entity) where T : class;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        DbRawSqlQuery<T> SqlQuery<T>(string sql, params object[] parameters) where T : class;

        /// <summary>
        /// Asynchronously execute given DDL/DML command.
        /// </summary>
        Task<int> ExecuteSqlCommandAsync(TransactionalBehavior transactionalBehavior, string sql, params object[] parameters);

        /// <summary>
        /// Asynchronously execute given DDL/DML command.
        /// </summary>
        Task<int> ExecuteSqlCommandAsync(string sql, params object[] parameters);
    }
}
