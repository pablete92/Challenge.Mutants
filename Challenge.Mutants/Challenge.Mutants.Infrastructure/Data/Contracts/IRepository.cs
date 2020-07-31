using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Challenge.Mutants.Infrastructure.Data.Contracts
{
    public interface IRepositoryQuery<TDbContext, TEntity>
           where TEntity : class
           where TDbContext : DbContext
    {
        IQueryable<TEntity> Queryable();

        IEnumerable<TEntity> GetAll();

        Task<IEnumerable<TEntity>> GetAllAsync();

        IList<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, params Expression<Func<TEntity, object>>[] includes);

        Task<IList<TEntity>> GetWithTake(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, int? take = null, params Expression<Func<TEntity, object>>[] includes);

        Task<IList<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, params Expression<Func<TEntity, object>>[] includes);

        IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null);

        TEntity GetById(params object[] keyValues);

        Task<TEntity> GetByIdAsync(params object[] keyValues);

        Task<TEntity> GetByIdAsync(CancellationToken cancellationToken, params object[] keyValues);

        TEntity GetFirstOrDefault(Expression<Func<TEntity, bool>> filter = null, params Expression<Func<TEntity, object>>[] includes);

        Task<TEntity> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> filter = null, params Expression<Func<TEntity, object>>[] includes);
    }

    public interface IRepositoryCommand<TDbContext, TEntity> : IRepositoryQuery<TDbContext, TEntity>
        where TEntity : class //EntityBase
        where TDbContext : DbContext
    {
        TEntity Create(TEntity entity);

        IEnumerable<TEntity> CreateRange(IEnumerable<TEntity> entities);

        void Delete(params object[] keyValues);

        void Delete(TEntity entityToDelete);

        TEntity Update(TEntity entityToUpdate);
    }
}
