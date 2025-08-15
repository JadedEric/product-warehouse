using Dapper;

using ProductWarehouse.Shared.Errors;

using System.Text;

namespace ProductWarehouse.Infrastructure.Repository;

public class ProductRepository(IDbConnectionFactory connectionFactory) : IProductRepository
{
    private readonly IDbConnectionFactory _connectionFactory = connectionFactory;

    public async Task<Result> AddAsync(Product product, CancellationToken cancellationToken = default)
    {
        try
        {
            using var connection = _connectionFactory.CreateConnection();

            const string sql = """
                INSERT INTO "Products" ("Id", "Name", "Description", "Price", "CreatedAt", "UpdatedAt")
                VALUES (@Id, @Name, @Description, @Price, @CreatedAt, @UpdatedAt)
                """;

            var rowsAffected = await connection.ExecuteAsync(sql, new
            {
                product.Id,
                product.Name,
                product.Description,
                product.Price,
                product.CreatedAt,
                product.UpdatedAt
            });

            return rowsAffected > 0
                ? Result.Success()
                : Result.Failure(new Error("Product.AddFailed", "Failed to add product to database"));
        }
        catch (Exception ex)
        {
            return Result.Failure(new Error("Product.AddException", $"Database error while adding product: {ex.Message}"));
        }
    }

    public async Task<Result> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        try
        {
            using var connection = _connectionFactory.CreateConnection();

            const string sql = """DELETE FROM "Products" WHERE "Id" = @Id""";

            var rowsAffected = await connection.ExecuteAsync(sql, new { Id = id });

            return rowsAffected > 0 ? Result.Success() : ProductErrors.NotFound;
        }
        catch (Exception ex)
        {
            return Result.Failure(new Error("Product.DeleteException", $"Database error while deleting product: {ex.Message}"));
        }
    }

    public async Task<Result<bool>> ExistsWithNameAsync(string name, Guid? excludeId = null, CancellationToken cancellationToken = default)
    {
        try
        {
            using var connection = _connectionFactory.CreateConnection();

            var sqlBuilder = new StringBuilder("""SELECT COUNT(1) FROM "Products" WHERE LOWER("Name") = LOWER(@Name)""");
            var paramDict = new Dictionary<string, object> { { "Name", name } };

            if (excludeId.HasValue)
            {
                sqlBuilder.Append(""" AND "Id" != @ExcludeId""");
                paramDict["ExcludeId"] = excludeId.Value;
            }

            var count = await connection.QuerySingleAsync<int>(sqlBuilder.ToString(), paramDict);
            return Result.Success(count > 0);
        }
        catch (Exception ex)
        {
            return Result.Failure<bool>(new Error("Product.ExistsCheckException", $"Database error while checking product name: {ex.Message}"));
        }
    }

    public async Task<Result<IReadOnlyList<Product>>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            using var connection = _connectionFactory.CreateConnection();

            const string sql = """
                SELECT "Id", "Name", "Description", "Price", "CreatedAt", "UpdatedAt"
                FROM "Products" 
                ORDER BY "CreatedAt" DESC
                """;

            var productsData = await connection.QueryAsync(sql);

            var products = productsData
                .Select(CreateProductFromData)
                .Where(p => p != null)
                .Cast<Product>()
                .ToList()
                .AsReadOnly();

            return Result.Success<IReadOnlyList<Product>>(products);
        }
        catch (Exception ex)
        {
            return Result.Failure<IReadOnlyList<Product>>(new Error("Product.GetAllException", $"Database error while retrieving products: {ex.Message}"));
        }
    }

    public async Task<Result<Product>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        try
        {
            using var connection = _connectionFactory.CreateConnection();

            const string sql = """
                SELECT "Id", "Name", "Description", "Price", "CreatedAt", "UpdatedAt"
                FROM "Products" 
                WHERE "Id" = @Id
                """;

            var productData = await connection.QuerySingleOrDefaultAsync(sql, new { Id = id });

            if (productData == null)
                return ProductErrors.NotFoundById(id);

            var product = CreateProductFromData(productData);
            return product != null
                ? Result.Success(product)
                : Result.Failure<Product>(new Error("Product.ReconstructionFailed", "Failed to reconstruct product from database"));
        }
        catch (Exception ex)
        {
            return Result.Failure<Product>(new Error("Product.GetException", $"Database error while retrieving product: {ex.Message}"));
        }
    }

    public async Task<Result> UpdateAsync(Product product, CancellationToken cancellationToken = default)
    {
        try
        {
            using var connection = _connectionFactory.CreateConnection();

            const string sql = """
                UPDATE "Products" 
                SET "Name" = @Name, "Description" = @Description, "Price" = @Price, "UpdatedAt" = @UpdatedAt
                WHERE "Id" = @Id
                """;

            var rowsAffected = await connection.ExecuteAsync(sql, new
            {
                product.Id,
                product.Name,
                product.Description,
                product.Price,
                product.UpdatedAt
            });

            return rowsAffected > 0 ? Result.Success() : ProductErrors.NotFound;
        }
        catch (Exception ex)
        {
            return Result.Failure(new Error("Product.UpdateException", $"Database error while updating product: {ex.Message}"));
        }
    }

    private static Product? CreateProductFromData(dynamic data)
    {
        try
        {
            return Product.FromRepository(
                (Guid)data.Id,
                (string)data.Name,
                (string)data.Description ?? string.Empty,
                (decimal)data.Price,
                (DateTime)data.CreatedAt,
                data.UpdatedAt as DateTime?
            );
        }
        catch
        {
            return null;
        }
    }
}
