using FastEndpoints;

using ProductWarehouse.Application.Interfaces;
using ProductWarehouse.Application.Records;
using ProductWarehouse.Shared.Extensions;
using ProductWarehouse.Shared.Interfaces;

namespace ProductWarehouse.Presentation.Product;

public class GetAllProductsEndpoint : EndpointWithoutRequest<IHttpResult<IReadOnlyList<ProductResponse>>>
{
    public IProductService ProductService { get; set; } = null!;

    public override void Configure()
    {
        Get("/products");
        AllowAnonymous();
        Summary(s =>
        {
            s.Summary = "Get all products";
            s.Description = "Retrieves all products ordered by creation date";
            s.Response<IReadOnlyList<ProductResponse>>(200, "Products retrieved successfully");
        });
    }

    public override async Task<IHttpResult<IReadOnlyList<ProductResponse>>> ExecuteAsync(CancellationToken ct)
    {
        var result = await ProductService.GetAllAsync(ct);
        return result.ToHttpResult();
    }
}