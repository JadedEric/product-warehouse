namespace ProductWarehouse.Application.Records;

public record UpdateProductRequest(
    string Name,
    string Description,
    decimal Price
);