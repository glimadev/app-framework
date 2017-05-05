using LinqKit;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Vitali.Framework.Repository.DataContext;
using Vitali.Framework.Repository.Infrastructure;
using Vitali.Framework.Repository.Repositories;
using Vitali.Framework.Repository.UnitOfWork;

namespace Vitali.Framework.Repository.Entity
{
    public class Repository<TEntity> : IRepositoryAsync<TEntity> where TEntity : class, IObjectState
    {
        #region [ Private Fields ]

        private readonly IDataContextAsync _context;
        private readonly DbSet<TEntity> _dbSet;
        private readonly IUnitOfWorkAsync _unitOfWork;
        private readonly Database _database;

        #endregion Private Fields

        public Repository(IDataContextAsync context, IUnitOfWorkAsync unitOfWork)
        {
            _context = context;
            _unitOfWork = unitOfWork;

            // Temporarily for FakeDbContext, Unit Test and Fakes
            var dbContext = context as DbContext;

            _database = dbContext.Database;

            if (dbContext != null)
            {
                _dbSet = dbContext.Set<TEntity>();
            }
            else
            {
                var fakeContext = context as FakeDbContext;

                if (fakeContext != null)
                {
                    _dbSet = fakeContext.Set<TEntity>();
                }
            }
        }

        #region [ Find ]

        public virtual TEntity FindById(params object[] keyValues)
        {
            return _dbSet.Find(keyValues);
        }

        public virtual TEntity Find(Expression<Func<TEntity, bool>> query)
        {
            return _dbSet.AsExpandable().Where(query).FirstOrDefault();
        }

        public virtual IEnumerable<TEntity> FindMany(Expression<Func<TEntity, bool>> query)
        {
            return _dbSet.AsExpandable().Where(query).AsEnumerable();
        }

        public virtual Task<TEntity> FindFirstAsync()
        {
            return _dbSet.FirstOrDefaultAsync();
        }

        public virtual async Task<TEntity> FindAsync(CancellationToken cancellationToken, params object[] keyValues)
        {
            return await _dbSet.FindAsync(cancellationToken, keyValues);
        }

        public virtual async Task<TEntity> FindByIdAsync(params object[] keyValues)
        {
            return await _dbSet.FindAsync(keyValues);
        }

        public virtual async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> query)
        {
            return await _dbSet.Where(query).FirstOrDefaultAsync();
        }

        public virtual async Task<List<TEntity>> FindManyAsync(Expression<Func<TEntity, bool>> query)
        {
            return await _dbSet.AsExpandable().Where(query).ToListAsync();
        }

        #endregion

        #region [ Select ]

        public virtual async Task<List<TEntity>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public virtual async Task<List<TEntity>> GetAllActivesAsync()
        {
            return await _dbSet.Where(x => x.Deleted == false && x.Active == true).ToListAsync();
        }

        #endregion

        public virtual IQueryable<TEntity> SelectQuery(string query, params object[] parameters)
        {
            return _dbSet.SqlQuery(query, parameters).AsQueryable();
        }

        public virtual TEntity Detach(TEntity entity)
        {
            entity.ObjectState = ObjectState.Detached;
            _context.SyncObjectState(entity);
            _context.SaveChanges();

            return entity;
        }

        public virtual TEntity Insert(TEntity entity)
        {
            entity.ObjectState = ObjectState.Added;
            entity.CreatedDate = DateTime.Now;
            _dbSet.Attach(entity);
            _context.SyncObjectState(entity);
            _context.SaveChanges();

            return entity;
        }

        public virtual TEntity InsertGraph(TEntity entity)
        {
            entity.CreatedDate = DateTime.Now;
            _dbSet.Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public virtual IEnumerable<TEntity> InsertRange(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                Insert(entity);
            }

            return entities;
        }

        public virtual Task<List<TElement>> SqlQueryToListAsync<TElement>(string sql, params object[] parameters)
        {
            return _database.SqlQuery<TElement>(sql, parameters).ToListAsync();
        }

        //public virtual Task ManageRelationship<TElement>(TEntity entity, List<TElement> tElements)
        //{
        //    Type type = entity.GetType();

        //    type.GetProperty("fsdf").SetValue(entity, null);
        //    type.GetProperty("fsdf").SetValue(entity, tElements);

        //    //_dbSet.RemoveRange(tElements);
        //    //_dbSet.AddRange(tElements);

        //    //_dbSet.Attach(entity);
        //    //_context.SyncObjectState(entity);
        //    //_context.SaveChanges();
        //}

        public virtual IEnumerable<TEntity> InsertGraphRange(IEnumerable<TEntity> entities)
        {
            entities.ForEach(x => x.CreatedDate = DateTime.Now);
            _dbSet.AddRange(entities);
            _context.SaveChanges();
            return entities;
        }

        public virtual IEnumerable<TEntity> UpdateGraphRange(IEnumerable<TEntity> entities)
        {
            entities.ForEach(x => x.EditedDate = DateTime.Now);
            AttachRange(entities);
            _context.SaveChanges();
            return entities;
        }

        public virtual void AttachRange(IEnumerable<TEntity> entities)
        {
            foreach (var item in entities)
	        {
                item.ObjectState = ObjectState.Modified;
                _dbSet.Attach(item);
	        }
        }

        public virtual TEntity Update(TEntity entity)
        {
            entity.ObjectState = ObjectState.Modified;
            entity.EditedDate = DateTime.Now;
            _dbSet.Attach(entity);
            _context.SyncObjectState(entity);
            _context.SaveChanges();
            /*try
            {
                _context.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                string errorMessages = string.Join("; ", ex.EntityValidationErrors.SelectMany(x => x.ValidationErrors).Select(x => x.ErrorMessage));
                throw new DbEntityValidationException(errorMessages);
            }*/
            return entity;
        }

        public virtual TEntity UnDeleteLogic(TEntity entity)
        {
            entity.ObjectState = ObjectState.Modified;
            entity.Deleted = false;
            _dbSet.Attach(entity);
            _context.SyncObjectState(entity);
            _context.SaveChanges();
            return entity;
        }

        public virtual TEntity DeleteLogic(TEntity entity)
        {
            entity.ObjectState = ObjectState.Modified;
            entity.DeletedDate = DateTime.Now;
            entity.Deleted = true;
            _dbSet.Attach(entity);
            _context.SyncObjectState(entity);
            _context.SaveChanges();
            return entity;
        }

        public virtual void Delete(object id)
        {
            var entity = _dbSet.Find(id);
            Delete(entity);
        }

        public virtual void Delete(TEntity entity)
        {
            entity.ObjectState = ObjectState.Deleted;
            _dbSet.Attach(entity);
            _context.SyncObjectState(entity);
            _context.SaveChanges();
        }

        public IQueryFluent<TEntity> Query()
        {
            return new QueryFluent<TEntity>(this);
        }

        public virtual IQueryFluent<TEntity> Query(IQueryObject<TEntity> queryObject)
        {
            return new QueryFluent<TEntity>(this, queryObject);
        }

        public virtual IQueryFluent<TEntity> Query(Expression<Func<TEntity, bool>> query)
        {
            return new QueryFluent<TEntity>(this, query);
        }

        public virtual IQueryFluent<TEntity> QueryActives(Expression<Func<TEntity, bool>> query = null)
        {
            query = (query == null) ? x => x.Active && !x.Deleted : query.And(x => x.Active && !x.Deleted);
            
            return new QueryFluent<TEntity>(this, query);
        }

        public IQueryable<TEntity> Queryable()
        {
            return _dbSet.AsExpandable();
        }

        public IRepository<T> GetRepository<T>() where T : class, IObjectState
        {
            return _unitOfWork.Repository<T>();
        }

        public virtual async Task<bool> DeleteAsync(params object[] keyValues)
        {
            return await DeleteAsync(CancellationToken.None, keyValues);
        }

        public virtual async Task<bool> DeleteAsync(CancellationToken cancellationToken, params object[] keyValues)
        {
            var entity = await FindAsync(cancellationToken, keyValues);

            if (entity == null)
            {
                return false;
            }

            entity.ObjectState = ObjectState.Deleted;
            _dbSet.Attach(entity);

            return true;
        }

        internal IQueryable<TEntity> Select(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            List<Expression<Func<TEntity, object>>> includes = null,
            int? page = null,
            int? pageSize = null,
            int? takeCount = null)
        {
            IQueryable<TEntity> query = _dbSet;

            if (includes != null)
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }
            if (orderBy != null)
            {
                query = orderBy(query);
            }
            if (filter != null)
            {
                query = query.AsExpandable().Where(filter);
            }
            if (takeCount != null)
            {
                query = query.Take(takeCount.Value);
            }
            else if (page != null && pageSize != null)
            {
                var count = (page.Value - 1) * pageSize.Value;
                query = query.Skip(count).Take(pageSize.Value);
            }
            return query;
        }

        internal async Task<TEntity> SelectAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            List<Expression<Func<TEntity, object>>> includes = null,
            int? page = null,
            int? pageSize = null)
        {
            return await Select(filter, orderBy, includes, page, pageSize).Take(1).FirstOrDefaultAsync();
        }

        internal async Task<IEnumerable<TEntity>> SelectManyAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            List<Expression<Func<TEntity, object>>> includes = null,
            int? page = null,
            int? pageSize = null)
        {
            return await Select(filter, orderBy, includes, page, pageSize).ToListAsync();
        }

        public virtual TEntity InsertOrUpdateGraph(TEntity entity)
        {
            SyncObjectGraph(entity);
            _entitesChecked = null;
            _dbSet.Attach(entity);
            return entity;
        }

        HashSet<object> _entitesChecked; // tracking of all process entities in the object graph when calling SyncObjectGraph

        private void SyncObjectGraph(object entity) // scan object graph for all 
        {
            if (_entitesChecked == null)
                _entitesChecked = new HashSet<object>();

            if (_entitesChecked.Contains(entity))
                return;

            _entitesChecked.Add(entity);

            var objectState = entity as IObjectState;

            if (objectState != null && objectState.ObjectState == ObjectState.Added)
                _context.SyncObjectState((IObjectState)entity);

            // Set tracking state for child collections
            foreach (var prop in entity.GetType().GetProperties())
            {
                // Apply changes to 1-1 and M-1 properties
                var trackableRef = prop.GetValue(entity, null) as IObjectState;
                if (trackableRef != null)
                {
                    if (trackableRef.ObjectState == ObjectState.Added)
                        _context.SyncObjectState((IObjectState)entity);

                    SyncObjectGraph(prop.GetValue(entity, null));
                }

                // Apply changes to 1-M properties
                var items = prop.GetValue(entity, null) as IEnumerable<IObjectState>;
                if (items == null) continue;

                Debug.WriteLine("Checking collection: " + prop.Name);

                foreach (var item in items)
                    SyncObjectGraph(item);
            }
        }
    }
}