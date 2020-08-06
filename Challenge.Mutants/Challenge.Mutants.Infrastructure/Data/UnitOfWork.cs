using Challenge.Mutants.Infrastructure.Data.Contracts;
using Challenge.Mutants.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Challenge.Mutants.Infrastructure.Data
{
    public class UnitOfWork<TDbContext> : IUnitOfWork<TDbContext>
           where TDbContext : DbContext
    {
        private bool disposed = false;

        private readonly TDbContext dbContext;
        private readonly IQueryManager queryManager;
        private readonly IAppUserService appUserService;

        public UnitOfWork(TDbContext dbContext, IQueryManager queryManager, IAppUserService appUserService)
        {
            this.dbContext = dbContext;
            this.queryManager = queryManager;
            this.appUserService = appUserService;
        }

        public Guid InstanceId { get; } = Guid.NewGuid();

        public TDbContext GetDbContext()
            => dbContext;

        public async Task SaveChangesAsync()
        {
            await SaveChangesAsync(CancellationToken.None);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            using var dbContextTransaction = await dbContext.Database.BeginTransactionAsync();
            try
            {
                dbContext.ChangeTracker.DetectChanges();
                var modified = dbContext.ChangeTracker.Entries().Where(x => x.State == EntityState.Added || x.State == EntityState.Modified || x.State == EntityState.Deleted);
                var updatingUser = appUserService.GetUserName();

                foreach (var item in modified)
                {
                    if (item.Entity is AuditEntityBase entity)
                    {
                        if (item.State == EntityState.Modified || item.State == EntityState.Deleted)
                        {
                            item.CurrentValues[nameof(AuditEntityBase.UpdatedAt)] = DateTime.Now;
                            item.CurrentValues[nameof(AuditEntityBase.UpdatedByName)] = updatingUser;
                        }
                        else if (item.State == EntityState.Added)
                        {
                            item.CurrentValues[nameof(AuditEntityBase.CreatedAt)] = DateTime.Now;
                            item.CurrentValues[nameof(AuditEntityBase.CreatedByName)] = updatingUser;
                        }
                    }
                }

                await dbContext.SaveChangesAsync(cancellationToken);
                dbContextTransaction.Commit();
            }
            catch
            {
                dbContextTransaction.Rollback();
                throw;
            }
        }

        public async Task<IEnumerable<TEntity>> InvokeEntityStoreProcedureAsync<TEntity>(string name, IDictionary<string, object> parameters, int? timeout = null)
            where TEntity : class//EntityBase
        {
            ValidateEntityInDbContext<TEntity>();

            if (timeout.HasValue)
            {
                dbContext.Database.SetCommandTimeout(timeout.Value);
            }

            this.queryManager.GenerateQuery(SqlCommandTypeInvocation.StoredProcedure, name, parameters);

            var result = await dbContext.Set<TEntity>().FromSqlRaw(this.queryManager.Query, this.queryManager.Parameters).ToListAsync();

            return result;
        }

        public async Task<int> InvokeStoreProcedureAsync(string name, IDictionary<string, object> parameters, int? timeout = null)
        {
            if (timeout.HasValue)
            {
                dbContext.Database.SetCommandTimeout(timeout.Value);
            }

            this.queryManager.GenerateQuery(SqlCommandTypeInvocation.StoredProcedure, name, parameters);

            int result = await dbContext.Database.ExecuteSqlRawAsync(this.queryManager.Query, this.queryManager.Parameters);

            return result;
        }

        public void Dispose()
        {
            this.Dispose(true);

            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    dbContext.Dispose();
                }
            }

            this.disposed = true;
        }

        private void ValidateEntityInDbContext<TEntity>()
            where TEntity : class
        {
            if (dbContext.Model.FindEntityType(typeof(TEntity)) == null)
            {
                throw new Exception($"The entity type {typeof(TEntity)} is not in the DbContext {typeof(TDbContext).Name}");
            }
        }

    }
}
