using Challenge.Mutants.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Challenge.Mutants.Domain.Mapping
{
    public class ADNMapping : IEntityTypeConfiguration<ADNEntity>
    {
        public void Configure(EntityTypeBuilder<ADNEntity> builder)
        {
            builder.HasKey(e => e.id);
            builder.ToTable("adn");

            builder.Property(e => e.adn).HasMaxLength(100).IsUnicode(false).IsRequired();
            builder.Property(e => e.mutant).IsRequired();
        }
    }
}
