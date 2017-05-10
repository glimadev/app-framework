using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using App.Framework.Repository.Infrastructure;
using App.Framework.Repository.Repositories;

namespace App.Framework.Repository.Service
{
    public interface IService<TEntity> where TEntity : IObjectState
    {
        #region [ Sync ]

        TEntity FindById(object keyValue);
        TEntity Find(Expression<Func<TEntity, bool>> query);
        IEnumerable<TEntity> FindMany(Expression<Func<TEntity, bool>> query);

        /*void Insert(TEntity entity);
        void InsertRange(IEnumerable<TEntity> entities);
        void InsertGraphRange(IEnumerable<TEntity> entities);
        void InsertOrUpdateGraph(TEntity entity);*/

        void Update(TEntity entity);
        void Delete(object id);
        void Delete(TEntity entity);

        #endregion

        #region [ Async ]
        
        Task<TEntity> FindByIdAsync(object keyValue);
        Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> query);
        Task<List<TEntity>> FindManyAsync(Expression<Func<TEntity, bool>> query);
        Task<TEntity> FindByIdAsync(CancellationToken cancellationToken, object keyValue);

        Task<List<TEntity>> GetAllAsync();

        Task<bool> DeleteAsync(params object[] keyValues);
        Task<bool> DeleteAsync(CancellationToken cancellationToken, params object[] keyValues);

        #endregion

        #region [ Query Fluent ]

        IQueryable<TEntity> SelectQuery(string query, params object[] parameters);
        IQueryFluent<TEntity> Query();
        IQueryFluent<TEntity> Query(IQueryObject<TEntity> queryObject);
        IQueryFluent<TEntity> Query(Expression<Func<TEntity, bool>> query);
        IQueryable<TEntity> Queryable();

        #endregion
    }
}
