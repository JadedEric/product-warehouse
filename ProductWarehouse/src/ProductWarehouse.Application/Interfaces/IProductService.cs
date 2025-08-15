using ProductWarehouse.Application.Records;
using ProductWarehouse.Shared.Common;

namespace ProductWarehouse.Application.Interfaces;

public interface IProductService
{
    Task<Result<ProductResponse>> CreateAsync(CreateProductRequest request, CancellationToken cancellationToken = default);
    
    Task<Result<ProductResponse>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    
    Task<Result<IReadOnlyList<ProductResponse>>> GetAllAsync(CancellationToken cancellationToken = default);
    
    Task<Result<ProductResponse>> UpdateAsync(Guid id, UpdateProductRequest request, CancellationToken cancellationToken = default);
    
    Task<Result> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}