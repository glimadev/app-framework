using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Vitali.Framework.Repository.Infrastructure;

namespace Vitali.Framework.Repository.Repositories
{
    public interface IQueryFluent<TEntity> where TEntity : IObjectState
    {
        IQueryFluent<TEntity> OrderBy(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy);
        IQueryFluent<TEntity> Include(Expression<Func<TEntity, object>> expression);
        IEnumerable<TEntity> SelectPage(int page, int pageSize, out int totalCount);
        IEnumerable<TEntity> SelectCount(int totalCount);
        IQueryFluent<TEntity> SelectTotal(out int totalCount);
        IEnumerable<TResult> Select<TResult>(Expression<Func<TEntity, TResult>> selector = null);
        IEnumerable<TEntity> Select();
        Task<TEntity> SelectAsync();
        Task<IEnumerable<TEntity>> SelectManyAsync();
        IQueryable<TEntity> SqlQuery(string query, params object[] parameters);
    }
}
