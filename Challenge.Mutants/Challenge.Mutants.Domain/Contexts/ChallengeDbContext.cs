using Challenge.Mutants.Domain.Entities;
using Challenge.Mutants.Domain.Mapping;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Challenge.Mutants.Domain.Contexts
{
    public class ChallengeDbContext : DbContext
    {
        public ChallengeDbContext() : base(new DbContextOptionsBuilder<ChallengeDbContext>()
            .UseSqlServer("Server=190.52.34.80;Port=5432;Database=dbMutant;User Id=postgres;Password=@@Praxis_1;Integrated Security=true;Pooling=true;")
            .Options)
        { }

        public ChallengeDbContext(DbContextOptions<ChallengeDbContext> option)
            : base(option) { }

        public virtual DbSet<ADNEntity> adn { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new ADNMapping());
        }

        public bool Exists<TEntity>() where TEntity : class
        {
            var attachedEntity = ChangeTracker.Entries<TEntity>().FirstOrDefault();
            return (attachedEntity != null);
        }
    }
}
