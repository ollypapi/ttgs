using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace TTGS.Core.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<TEntity> FirstOrDefaultAsync();
        TEntity FirstOrDefault();
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate);
        Task<List<TEntity>> GetListAsync();
        IQueryable<TEntity> AsQueryable();
        Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate);
        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate);
        Task<int> CountAsync();
        Task InsertAsync(TEntity entity);
        void Delete(TEntity entityToDelete);
        void Delete(IEnumerable<TEntity> entitiesToDelete);
        void Update(TEntity entityToUpdate);
    }
}