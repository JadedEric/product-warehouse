using Microsoft.EntityFrameworkCore;

namespace ProductWarehouse.Infrastructure.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Product> Products { get; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    
    int SaveChanges();
}