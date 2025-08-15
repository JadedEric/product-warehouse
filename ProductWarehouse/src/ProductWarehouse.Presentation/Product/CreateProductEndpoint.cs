using FastEndpoints;

using ProductWarehouse.Application.Interfaces;
using ProductWarehouse.Application.Records;
using ProductWarehouse.Shared.Extensions;
using ProductWarehouse.Shared.Http;
using ProductWarehouse.Shared.Interfaces;

namespace ProductWarehouse.Presentation.Product;

public class CreateProductEndpoint : Endpoint<CreateProductRequest, IHttpResult<ProductResponse>>
{
    public IProductService ProductService { get; set; } = null!;

    public override void Configure()
    {
        Post("/products");
        AllowAnonymous();
        Summary(s =>
        {
            s.Summary = "Create a new product";
            s.Description = "Creates a new product with the provided details";
            s.Response<ProductResponse>(201, "Product created successfully");
            s.Response<ProblemDetails>(400, "Invalid product data");
            s.Response<ProblemDetails>(409, "Product with same name already exists");
        });
    }

    public override async Task<IHttpResult<ProductResponse>> ExecuteAsync(CreateProductRequest req, CancellationToken ct)
    {
        var result = await ProductService.CreateAsync(req, ct);

        return (IHttpResult<ProductResponse>)result.ToHttpResult(value => HttpResult<ProductResponse>.Created(value));
    }
}
