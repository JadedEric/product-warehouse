namespace ProductWarehouse.Application.Records;

public record CreateProductRequest(
    string Name,
    string Description,
    decimal Price
);