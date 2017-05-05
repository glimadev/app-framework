using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Vitali.Framework.Repository.Infrastructure;
using Vitali.Framework.Repository.Repositories;

namespace Vitali.Framework.Repository.Service
{
    public abstract class Service<TEntity> : IService<TEntity> where TEntity : class, IObjectState
    {
        #region [ Private Fields ]
        private readonly IRepositoryAsync<TEntity> _repository;
        #endregion Private Fields

        #region [ Constructor ]

        protected Service(IRepositoryAsync<TEntity> repository)
        {
            _repository = repository;
        }

        #endregion Constructor

        #region [ Find ]

        public virtual TEntity FindById(object keyValue)
        {
            return _repository.FindById(keyValue);
        }

        public TEntity Find(Expression<Func<TEntity, bool>> query)
        {
            return _repository.Find(query);
        }

        public Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> query)
        {
            return _repository.FindAsync(query);
        }

        public Task<List<TEntity>> FindManyAsync(Expression<Func<TEntity, bool>> query)
        {
            return _repository.FindManyAsync(query);
        }

        public IEnumerable<TEntity> FindMany(Expression<Func<TEntity, bool>> query)
        {
            return _repository.FindMany(query);
        }

        #endregion

        #region [ Select ]

        public Task<List<TEntity>> GetAllAsync()
        {
            return _repository.GetAllAsync();
        }

        #endregion

        public virtual IQueryable<TEntity> SelectQuery(string query, params object[] parameters) { return _repository.SelectQuery(query, parameters).AsQueryable(); }

        public virtual void Insert(TEntity entity) { _repository.Insert(entity); }
        /*
        public virtual void InsertRange(IEnumerable<TEntity> entities) { _repository.InsertRange(entities); }

        public virtual void InsertOrUpdateGraph(TEntity entity) { _repository.InsertOrUpdateGraph(entity); }

        public virtual void InsertGraphRange(IEnumerable<TEntity> entities) { _repository.InsertGraphRange(entities); }*/

        public virtual void Update(TEntity entity) { _repository.Update(entity); }

        public virtual void Delete(object id) { _repository.Delete(id); }

        public virtual void Delete(TEntity entity) { _repository.Delete(entity); }

        public IQueryFluent<TEntity> Query() { return _repository.Query(); }

        public virtual IQueryFluent<TEntity> Query(IQueryObject<TEntity> queryObject) { return _repository.Query(queryObject); }

        public virtual IQueryFluent<TEntity> Query(Expression<Func<TEntity, bool>> query) { return _repository.Query(query); }

        public virtual async Task<TEntity> FindByIdAsync(object keyValue) { return await _repository.FindByIdAsync(keyValue); }

        public virtual async Task<TEntity> FindByIdAsync(CancellationToken cancellationToken, object keyValue) { return await _repository.FindAsync(cancellationToken, keyValue); }

        public virtual async Task<bool> DeleteAsync(params object[] keyValues) { return await DeleteAsync(CancellationToken.None, keyValues); }

        //IF 04/08/2014 - Before: return await DeleteAsync(cancellationToken, keyValues);
        public virtual async Task<bool> DeleteAsync(CancellationToken cancellationToken, params object[] keyValues) { return await _repository.DeleteAsync(cancellationToken, keyValues); }

        public IQueryable<TEntity> Queryable() { return _repository.Queryable(); }
    }
}