using Microsoft.EntityFrameworkCore;

using ProductWarehouse.Core.Entities;
using ProductWarehouse.Infrastructure.Interfaces;

namespace ProductWarehouse.Infrastructure.Context;

public class ProductDbContext(DbContextOptions<ProductDbContext> options) : DbContext(options), IApplicationDbContext
{
    public DbSet<Product> Products => Set<Product>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}
