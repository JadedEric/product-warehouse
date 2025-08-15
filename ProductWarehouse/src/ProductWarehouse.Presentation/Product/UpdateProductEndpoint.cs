using FastEndpoints;

using ProductWarehouse.Application.Interfaces;
using ProductWarehouse.Application.Records;
using ProductWarehouse.Presentation.Models;
using ProductWarehouse.Shared.Extensions;
using ProductWarehouse.Shared.Interfaces;

namespace ProductWarehouse.Presentation.Product;

public class UpdateProduct : Endpoint<UpdateProductEndpointRequest, IHttpResult<ProductResponse>>
{
    public IProductService ProductService { get; set; } = null!;

    public override void Configure()
    {
        Put("/products/{id}");
        AllowAnonymous();
        Summary(s =>
        {
            s.Summary = "Update a product";
            s.Description = "Updates an existing product with new details";
            s.Response<ProductResponse>(200, "Product updated successfully");
            s.Response<ProblemDetails>(400, "Invalid product data");
            s.Response<ProblemDetails>(404, "Product not found");
            s.Response<ProblemDetails>(409, "Product with same name already exists");
        });
    }

    public override async Task<IHttpResult<ProductResponse>> ExecuteAsync(UpdateProductEndpointRequest req, CancellationToken ct)
    {
        var updateRequest = new UpdateProductRequest(req.Name, req.Description, req.Price);
        var result = await ProductService.UpdateAsync(req.Id, updateRequest, ct);

        return result.ToHttpResult();
    }
}