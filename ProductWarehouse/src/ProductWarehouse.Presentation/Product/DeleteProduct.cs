using FastEndpoints;

using ProductWarehouse.Application.Interfaces;
using ProductWarehouse.Presentation.Models;
using ProductWarehouse.Shared.Extensions;
using ProductWarehouse.Shared.Interfaces;

namespace ProductWarehouse.Presentation.Product;

public class DeleteProduct : Endpoint<DeleteProductRequest, IHttpResult>
{
    public IProductService ProductService { get; set; } = null!;

    public override void Configure()
    {
        Delete("/products/{id}");
        AllowAnonymous();
        Summary(s =>
        {
            s.Summary = "Delete a product";
            s.Description = "Deletes a product by its unique identifier";
            s.Response(204, "Product deleted successfully");
            s.Response<ProblemDetails>(404, "Product not found");
        });
    }

    public override async Task<IHttpResult> ExecuteAsync(DeleteProductRequest req, CancellationToken ct)
    {
        var result = await ProductService.DeleteAsync(req.Id, ct);
        return result.ToHttpResult();
    }
}
