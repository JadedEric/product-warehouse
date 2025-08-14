using ProductWarehouse.Shared.Common;

namespace ProductWarehouse.Shared.Errors;

public static class ProductErrors
{
    public static readonly Error NotFound = new("Product.NotFound", "The product was not found");

    public static Error NotFoundById(Guid id) =>
        Error.NotFound("Product", id);

    public static readonly Error InvalidName = new("Product.InvalidName", "Product name cannot be empty or whitespace");


    public static readonly Error InvalidPrice = new("Product.InvalidPrice", "Product price must be greater than zero");


    public static readonly Error DuplicateName = new("Product.DuplicateName", "A product with this name already exists");


    public static readonly Error CannotDelete = new("Product.CannotDelete", "Cannot delete product that has active orders");
}