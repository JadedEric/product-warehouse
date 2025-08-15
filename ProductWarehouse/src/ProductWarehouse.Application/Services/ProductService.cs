using ProductWarehouse.Application.Interfaces;
using ProductWarehouse.Application.Records;
using ProductWarehouse.Core.Entities;
using ProductWarehouse.Core.Interfaces;
using ProductWarehouse.Shared.Common;
using ProductWarehouse.Shared.Errors;

namespace ProductWarehouse.Application.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Result<ProductResponse>> CreateAsync(CreateProductRequest request, CancellationToken cancellationToken = default)
    {
        // Check if product with same name already exists
        var existsResult = await _productRepository.ExistsWithNameAsync(request.Name, cancellationToken: cancellationToken);
        if (existsResult.IsFailure)
            return Result.Failure<ProductResponse>(existsResult.Error);

        if (existsResult.Value)
            return Result.Failure<ProductResponse>(ProductErrors.DuplicateName);

        // Create the product using domain logic
        var productResult = Product.Create(request.Name, request.Description, request.Price);
        if (productResult.IsFailure)
            return Result.Failure<ProductResponse>(productResult.Error);

        // Save to repository
        var addResult = await _productRepository.AddAsync(productResult.Value, cancellationToken);
        if (addResult.IsFailure)
            return Result.Failure<ProductResponse>(addResult.Error);

        // Return the created product as response
        return Result.Success(ToProductResponse(productResult.Value));
    }

    public async Task<Result<ProductResponse>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await _productRepository.GetByIdAsync(id, cancellationToken);

        return result.IsSuccess
            ? Result.Success(ToProductResponse(result.Value))
            : Result.Failure<ProductResponse>(result.Error);
    }

    public async Task<Result<IReadOnlyList<ProductResponse>>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var result = await _productRepository.GetAllAsync(cancellationToken);

        if (result.IsFailure)
            return Result.Failure<IReadOnlyList<ProductResponse>>(result.Error);

        var responses = result.Value.Select(ToProductResponse).ToList().AsReadOnly();
        return Result.Success<IReadOnlyList<ProductResponse>>(responses);
    }

    public async Task<Result<ProductResponse>> UpdateAsync(Guid id, UpdateProductRequest request, CancellationToken cancellationToken = default)
    {
        // Get existing product
        var existingProductResult = await _productRepository.GetByIdAsync(id, cancellationToken);
        if (existingProductResult.IsFailure)
            return Result.Failure<ProductResponse>(existingProductResult.Error);

        var existingProduct = existingProductResult.Value;

        // Check if another product with same name exists (excluding current product)
        var existsResult = await _productRepository.ExistsWithNameAsync(request.Name, id, cancellationToken);
        if (existsResult.IsFailure)
            return Result.Failure<ProductResponse>(existsResult.Error);

        if (existsResult.Value)
            return Result.Failure<ProductResponse>(ProductErrors.DuplicateName);

        // Update using domain logic
        var updateResult = existingProduct.Update(request.Name, request.Description, request.Price);
        if (updateResult.IsFailure)
            return Result.Failure<ProductResponse>(updateResult.Error);

        // Save changes
        var saveResult = await _productRepository.UpdateAsync(existingProduct, cancellationToken);
        if (saveResult.IsFailure)
            return Result.Failure<ProductResponse>(saveResult.Error);

        return Result.Success(ToProductResponse(existingProduct));
    }

    public async Task<Result> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _productRepository.DeleteAsync(id, cancellationToken);
    }

    private static ProductResponse ToProductResponse(Product product)
    {
        return new ProductResponse(
            product.Id,
            product.Name,
            product.Description,
            product.Price,
            product.CreatedAt,
            product.UpdatedAt
        );
    }
}