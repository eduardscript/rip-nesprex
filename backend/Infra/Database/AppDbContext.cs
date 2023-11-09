using Domain.Entities;
using Infra.Database.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Database;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>()
            .HasMany(x => x.Capsules)
            .WithOne(x => x.Category)
            .HasForeignKey(x => x.CategoryId)
            .IsRequired();

        modelBuilder.Entity<Capsule>()
            .HasMany(x => x.AuditLogs)
            .WithOne(x => x.Capsule)
            .HasForeignKey(x => x.CapsuleId);

        modelBuilder.Entity<Capsule>()
            .HasOne<Specification>()
            .WithOne(x => x.Capsule)
            .HasForeignKey<Specification>(x => x.CapsuleId);

        modelBuilder.Entity<Specification>()
            .HasOne(x => x.Capsule)
            .WithOne(x => x.Specification)
            .HasForeignKey<Specification>(x => x.CapsuleId);

        modelBuilder.Seed();

        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Category> Categories => Set<Category>();

    public DbSet<Capsule> Capsules => Set<Capsule>();

    public DbSet<Specification> Specifications => Set<Specification>();

    public DbSet<AuditLog> AuditLogs => Set<AuditLog>();
}