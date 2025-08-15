using ProductWarehouse.Shared.Common;

namespace ProductWarehouse.Shared.Errors;

public static class ProductErrors
{
    public static readonly Error NotFound = new("NotFound.Product", "The product was not found");

    public static Error NotFoundById(Guid id) =>
        new("NotFound.Product", $"Product with ID '{id}' was not found");

    
    public static readonly Error InvalidName = new("Validation.Product.Name", "Product name cannot be empty or whitespace");

    
    public static readonly Error InvalidPrice = new("Validation.Product.Price", "Product price must be greater than zero");

    
    public static readonly Error DuplicateName = new("Conflict.Product.Name", "A product with this name already exists");

    
    public static readonly Error CannotDelete = new("Conflict.Product.Delete", "Cannot delete product that has active orders");
}