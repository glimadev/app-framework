using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Vitali.Framework.Repository.Infrastructure;

namespace Vitali.Framework.Repository.Repositories
{
    public interface IRepository<TEntity> where TEntity : class, IObjectState
    {
        TEntity UnDeleteLogic(TEntity entity);
        TEntity DeleteLogic(TEntity entity);

        TEntity FindById(params object[] keyValues);
        TEntity Find(Expression<Func<TEntity, bool>> query);
        IEnumerable<TEntity> FindMany(Expression<Func<TEntity, bool>> query);

        TEntity Insert(TEntity entity);
        IEnumerable<TEntity> InsertRange(IEnumerable<TEntity> entities);
        TEntity InsertOrUpdateGraph(TEntity entity);
        TEntity InsertGraph(TEntity entity);
        IEnumerable<TEntity> InsertGraphRange(IEnumerable<TEntity> entities);
        IEnumerable<TEntity> UpdateGraphRange(IEnumerable<TEntity> entities);

        TEntity Update(TEntity entity);

        void Delete(object id);
        void Delete(TEntity entity);

        IQueryable<TEntity> SelectQuery(string query, params object[] parameters);
        IQueryFluent<TEntity> Query(IQueryObject<TEntity> queryObject);
        IQueryFluent<TEntity> Query(Expression<Func<TEntity, bool>> query);
        IQueryFluent<TEntity> QueryActives(Expression<Func<TEntity, bool>> query = null);
        IQueryFluent<TEntity> Query();
        IQueryable<TEntity> Queryable();
        IRepository<T> GetRepository<T>() where T : class, IObjectState;
        
        TEntity Detach(TEntity entity);
    }
}
