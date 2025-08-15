namespace ProductWarehouse.Application.Records;

public record ProductResponse(
    Guid Id,
    string Name,
    string Description,
    decimal Price,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);