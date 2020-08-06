using Challenge.Mutants.Infrastructure.Data.Contracts;
using Challenge.Mutants.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Challenge.Mutants.Infrastructure.Data
{
    public class Repository<TDbContext, TEntity> : IRepositoryCommand<TDbContext, TEntity>
           where TEntity : EntityBase
           where TDbContext : DbContext
    {
        private readonly bool traking = false;
        private readonly DbContext context;
        private readonly DbSet<TEntity> dbentitySet;

        public Repository(TDbContext context, bool traking)
        {
            this.context = context;
            ValidateEntityInDbContext();

            this.traking = traking;
            this.dbentitySet = context.Set<TEntity>();
        }

        public virtual TEntity Create(TEntity entity)
        {
            var newEntity = this.dbentitySet.Add(entity);

            return newEntity.Entity;
        }

        public virtual IEnumerable<TEntity> CreateRange(IEnumerable<TEntity> entities)
        {
            var newEntities = new List<TEntity>();

            foreach (var entity in entities)
            {
                newEntities.Add(this.Create(entity));
            }

            return newEntities;
        }

        public virtual void Delete(params object[] keyValues)
        {
            TEntity entityToDelete = this.dbentitySet.Find(keyValues);

            if (entityToDelete == null)
            {
                var name = typeof(TEntity).Name;

                throw new ApplicationException($"La entidad '{name}' con ID {string.Join(",", keyValues)} no existe.");
            }

            this.Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (this.context.Entry(entityToDelete).State == EntityState.Detached)
            {
                this.dbentitySet.Attach(entityToDelete);
            }

            this.dbentitySet.Remove(entityToDelete);
        }

        public virtual TEntity Update(TEntity entityToUpdate)
        {
            var local = context.Set<TEntity>().Local.FirstOrDefault(e => e.ID.Equals(entityToUpdate.ID));

            if (local != null)
            {
                context.Entry(local).State = EntityState.Detached;
            }

            var entityUpdated = this.dbentitySet.Update(entityToUpdate);

            this.context.Entry(entityToUpdate).State = EntityState.Modified;

            return entityUpdated.Entity;
        }

        public virtual IQueryable<TEntity> Queryable()
        {
            return this.dbentitySet.WithTracking(this.traking);
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return this.dbentitySet.WithTracking(this.traking).AsEnumerable<TEntity>().ToList();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await this.dbentitySet.WithTracking(this.traking).ToListAsync<TEntity>();
        }

        public virtual IList<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, params Expression<Func<TEntity, object>>[] includes)
        {
            return this.GetQuery(filter, orderBy, includes).ToList();
        }

        public virtual async Task<IList<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, params Expression<Func<TEntity, object>>[] includes)
        {
            return await this.GetQuery(filter, orderBy, includes).ToListAsync();
        }

        public virtual IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null)
        {
            IQueryable<TEntity> query = this.dbentitySet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return query.WithTracking(this.traking);
        }

        public virtual TEntity GetById(params object[] keyValues)
        {
            var entity = this.dbentitySet.Find(keyValues);

            if (!traking)
            {
                context.Entry(entity).State = EntityState.Detached;
            }

            return entity;
        }

        public virtual async Task<TEntity> GetByIdAsync(params object[] keyValues)
        {
            var entity = await this.dbentitySet.FindAsync(keyValues);

            if (!traking)
            {
                context.Entry(entity).State = EntityState.Detached;
            }

            return entity;
        }

        public virtual async Task<TEntity> GetByIdAsync(CancellationToken cancellationToken, params object[] keyValues)
        {
            var entity = await this.dbentitySet.FindAsync(cancellationToken, keyValues);

            if (!traking)
            {
                context.Entry(entity).State = EntityState.Detached;
            }

            return entity;
        }

        public virtual TEntity GetFirstOrDefault(Expression<Func<TEntity, bool>> filter = null, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = this.dbentitySet;

            foreach (Expression<Func<TEntity, object>> include in includes)
            {
                query = query.Include(include);
            }

            return query.WithTracking(this.traking).FirstOrDefault(filter);
        }

        public virtual async Task<TEntity> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> filter = null, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = this.dbentitySet;

            foreach (Expression<Func<TEntity, object>> include in includes)
            {
                query = query.Include(include);
            }

            return await query.WithTracking(this.traking).FirstOrDefaultAsync(filter);
        }

        private IQueryable<TEntity> GetQuery(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = this.dbentitySet;

            foreach (Expression<Func<TEntity, object>> include in includes)
            {
                query = query.Include(include);
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return query.WithTracking(this.traking);
        }

        private void ValidateEntityInDbContext()
        {
            if (context.Model.FindEntityType(typeof(TEntity)) == null)
            {
                throw new Exception($"The entity type {typeof(TEntity)} is not in the DbContext {typeof(TDbContext).Name}");
            }
        }

        public async Task<IList<TEntity>> GetWithTake(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, int? take = null, params Expression<Func<TEntity, object>>[] includes)
        {
            return await this.GetQuery(filter, orderBy, includes).Take(take == null ? 1 : int.Parse(take.ToString())).ToListAsync();
        }
    }
}
