using ProductWarehouse.Shared.Common;
using ProductWarehouse.Shared.Errors;

namespace ProductWarehouse.Core.Entities;

public class Product
{
    public Guid Id { get; private set; }
    
    public string Name { get; private set; } = string.Empty;
    
    public string Description { get; private set; } = string.Empty;
    
    public decimal Price { get; private set; }
    
    public DateTime CreatedAt { get; private set; }
    
    public DateTime? UpdatedAt { get; private set; }

    private Product()
    { 

    }

    private Product(Guid id, string name, string description, decimal price, DateTime createdAt)
    {
        Id = id;
        Name = name;
        Description = description;
        Price = price;
        CreatedAt = createdAt;
    }

    public static Result<Product> Create(string name, string description, decimal price)
    {
        if (string.IsNullOrWhiteSpace(name))
            return ProductErrors.InvalidName;

        if (price <= 0)
            return ProductErrors.InvalidPrice;

        return new Product(
            Guid.NewGuid(),
            name.Trim(),
            description?.Trim() ?? string.Empty,
            price,
            DateTime.UtcNow);
    }

    public Result Update(string name, string description, decimal price)
    {
        if (string.IsNullOrWhiteSpace(name))
            return ProductErrors.InvalidName;

        if (price <= 0)
            return ProductErrors.InvalidPrice;

        Name = name.Trim();
        Description = description?.Trim() ?? string.Empty;
        Price = price;
        UpdatedAt = DateTime.UtcNow;

        return Result.Success();
    }

    public static Product FromRepository(Guid id, string name, string description, decimal price, DateTime createdAt, DateTime? updatedAt)
    {
        return new Product(id, name, description, price, createdAt)
        {
            UpdatedAt = updatedAt
        };
    }
}