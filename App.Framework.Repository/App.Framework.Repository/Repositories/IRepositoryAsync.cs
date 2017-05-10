using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using App.Framework.Repository.Infrastructure;

namespace App.Framework.Repository.Repositories
{
    public interface IRepositoryAsync<TEntity> : IRepository<TEntity> where TEntity : class, IObjectState
    {
        Task<TEntity> FindFirstAsync();
        Task<TEntity> FindByIdAsync(params object[] keyValues);
        Task<TEntity> FindAsync(CancellationToken cancellationToken, params object[] keyValues);
        Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> query);
        
        Task<List<TEntity>> FindManyAsync(Expression<Func<TEntity, bool>> query);
        Task<List<TEntity>> GetAllAsync();
        Task<List<TEntity>> GetAllActivesAsync();

        Task<List<TElement>> SqlQueryToListAsync<TElement>(string sql, params object[] parameters);
        Task<bool> DeleteAsync(params object[] keyValues);
        Task<bool> DeleteAsync(CancellationToken cancellationToken, params object[] keyValues);
    }
}
