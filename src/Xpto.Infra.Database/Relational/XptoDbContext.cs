using Microsoft.EntityFrameworkCore;
using System.Data;
using Xpto.Domain.Entities;

namespace Xpto.Infra.Database.Relational;

public class XptoDbContext(DbContextOptions<XptoDbContext> options) : DbContext(options), IUnitOfWork
{
    public DbSet<Wallet> Wallets { get; set; }
    public DbSet<Asset> Assets { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var assembly = typeof(XptoDbContext).Assembly;
        modelBuilder.ApplyConfigurationsFromAssembly(assembly);

        base.OnModelCreating(modelBuilder);
    }

    public async Task<bool> Commit()
    {
        foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("CreatedAt") is not null))
        {
            if (entry.State == EntityState.Added)
            {
                entry.Property("CreatedAt").CurrentValue = DateTime.Now;
                entry.Property("UpdatedAt").CurrentValue = DateTime.Now;
            }

            if (entry.State == EntityState.Modified)
            {
                entry.Property("CreatedAt").IsModified = false;
                entry.Property("UpdatedAt").CurrentValue = DateTime.Now;
            }
        }

        var success = await base.SaveChangesAsync() > 0;

        return success;
    }
}
