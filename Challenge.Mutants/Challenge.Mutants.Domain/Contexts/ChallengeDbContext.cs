﻿using Challenge.Mutants.Domain.Entities;
using Challenge.Mutants.Domain.Mapping;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Challenge.Mutants.Domain.Contexts
{
    public class ChallengeDbContext : DbContext
    {
        public ChallengeDbContext() : base(new DbContextOptionsBuilder<ChallengeDbContext>()
            .UseSqlServer("Data Source=DESKTOP-HCK3H43\\SQLEXPRESS01;Initial Catalog=Mutants;Integrated Security=True;")
            .Options)
        { }

        public ChallengeDbContext(DbContextOptions<ChallengeDbContext> option)
            : base(option) { }

        public virtual DbSet<ADNEntity> ADN { get; set; }

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