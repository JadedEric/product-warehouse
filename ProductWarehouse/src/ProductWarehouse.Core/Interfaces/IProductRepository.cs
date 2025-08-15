using ProductWarehouse.Core.Entities;
using ProductWarehouse.Shared.Common;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductWarehouse.Core.Interfaces;

public interface IProductRepository
{
    Task<Result<Product>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    
    Task<Result<IReadOnlyList<Product>>> GetAllAsync(CancellationToken cancellationToken = default);
    
    Task<Result<bool>> ExistsWithNameAsync(string name, Guid? excludeId = null, CancellationToken cancellationToken = default);
    
    Task<Result> AddAsync(Product product, CancellationToken cancellationToken = default);
    
    Task<Result> UpdateAsync(Product product, CancellationToken cancellationToken = default);
    
    Task<Result> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
