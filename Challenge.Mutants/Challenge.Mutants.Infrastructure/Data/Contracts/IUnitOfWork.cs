using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Challenge.Mutants.Infrastructure.Data.Contracts
{
    public interface IUnitOfWork<TDbContext> : IStoredProcedureInvoker, IDisposable
        where TDbContext : DbContext
    {
        Guid InstanceId { get; }

        TDbContext GetDbContext();

        Task SaveChangesAsync();

        Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}
