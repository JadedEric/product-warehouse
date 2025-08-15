using FastEndpoints;

using ProductWarehouse.Application.Interfaces;
using ProductWarehouse.Application.Records;
using ProductWarehouse.Presentation.Models;
using ProductWarehouse.Shared.Extensions;
using ProductWarehouse.Shared.Interfaces;

namespace ProductWarehouse.Presentation.Product;

public class GetProductByIdEndpoint : Endpoint<GetProductByIdRequest, IHttpResult<ProductResponse>>
{
    public IProductService ProductService { get; set; } = null!;

    public override void Configure()
    {
        Get("/products/{id}");
        AllowAnonymous();
        Summary(s =>
        {
            s.Summary = "Get product by ID";
            s.Description = "Retrieves a product by its unique identifier";
            s.Response<ProductResponse>(200, "Product found");
            s.Response<ProblemDetails>(404, "Product not found");
        });
    }

    public override async Task<IHttpResult<ProductResponse>> ExecuteAsync(GetProductByIdRequest req, CancellationToken ct)
    {
        var result = await ProductService.GetByIdAsync(req.Id, ct);
        return result.ToHttpResult();
    }
}
