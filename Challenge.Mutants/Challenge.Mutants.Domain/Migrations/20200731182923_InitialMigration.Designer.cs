﻿// <auto-generated />
using Challenge.Mutants.Domain.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Challenge.Mutants.Domain.Migrations
{
    [DbContext(typeof(ChallengeDbContext))]
    [Migration("20200731182923_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Challenge.Mutants.Domain.Entities.ADNEntity", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Adn")
                        .IsRequired()
                        .HasColumnType("varchar(41)")
                        .HasMaxLength(41)
                        .IsUnicode(false);

                    b.Property<bool>("Mutant")
                        .HasColumnType("bit");

                    b.HasKey("ID");

                    b.ToTable("ADN");
                });
#pragma warning restore 612, 618
        }
    }
}
