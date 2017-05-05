using System.Threading;
using System.Threading.Tasks;
using Vitali.Framework.Repository.Infrastructure;
using Vitali.Framework.Repository.Repositories;

namespace Vitali.Framework.Repository.UnitOfWork
{
    public interface IUnitOfWorkAsync : IUnitOfWork
    {
        Task<int> SaveChangesAsync();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        IRepositoryAsync<TEntity> RepositoryAsync<TEntity>() where TEntity : class, IObjectState;
    }
}
