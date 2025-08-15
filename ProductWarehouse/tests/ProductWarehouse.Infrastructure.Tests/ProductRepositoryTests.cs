using FluentAssertions;

using ProductWarehouse.Core.Entities;
using ProductWarehouse.Infrastructure.Repository;
using ProductWarehouse.Shared.Errors;

namespace ProductWarehouse.Infrastructure.Tests;

public class ProductRepositoryTests: DatabaseTestBase
{
    private ProductRepository _repository = null!;

    public override async Task InitializeAsync()
    {
        await base.InitializeAsync();

        _repository = new ProductRepository(ConnectionFactory);
    }

    [Fact]
    public async Task AddAsync_WithValidProduct_ShouldReturnSuccess()
    {
        // Arrange
        var productResult = Product.Create("Test Product", "Test Description", 99.99m);
        productResult.IsSuccess.Should().BeTrue();
        var product = productResult.Value;

        // Act
        var result = await _repository.AddAsync(product);

        // Assert
        result.IsSuccess.Should().BeTrue();

        var retrievedResult = await _repository.GetByIdAsync(product.Id);

        retrievedResult.IsSuccess.Should().BeTrue();
        retrievedResult.Value.Name.Should().Be("Test Product");
    }

    [Fact]
    public async Task GetByIdAsync_WithNonExistentId_ShouldReturnNotFound()
    {
        // Arrange
        var nonExistentId = Guid.NewGuid();

        // Act
        var result = await _repository.GetByIdAsync(nonExistentId);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Code.Should().Be("NotFound.Product");
    }

    [Fact]
    public async Task GetByIdAsync_WithExistingId_ShouldReturnProduct()
    {
        // Arrange
        var productResult = Product.Create("Test Product", "Test Description", 99.99m);
        productResult.IsSuccess.Should().BeTrue();
        var product = productResult.Value;
        await _repository.AddAsync(product);

        // Act
        var result = await _repository.GetByIdAsync(product.Id);
                
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value.Id.Should().Be(product.Id);
        result.Value.Name.Should().Be("Test Product");
        result.Value.Description.Should().Be("Test Description");
        result.Value.Price.Should().Be(99.99m);
    }

    [Fact]
    public async Task UpdateAsync_WithValidProduct_ShouldReturnSuccess()
    {
        // Arrange
        var productResult = Product.Create("Original Name", "Original Description", 50.00m);
        productResult.IsSuccess.Should().BeTrue();
        var product = productResult.Value;
        await _repository.AddAsync(product);

        var updateResult = product.Update("Updated Name", "Updated Description", 75.00m);
        updateResult.IsSuccess.Should().BeTrue();

        // Act
        var result = await _repository.UpdateAsync(product);

        // Assert
        result.IsSuccess.Should().BeTrue();
       
        var retrievedResult = await _repository.GetByIdAsync(product.Id);

        retrievedResult.Value.Name.Should().Be("Updated Name");
        retrievedResult.Value.Description.Should().Be("Updated Description");
        retrievedResult.Value.Price.Should().Be(75.00m);
        retrievedResult.Value.UpdatedAt.Should().NotBeNull();
    }

    [Fact]
    public async Task UpdateAsync_WithNonExistentProduct_ShouldReturnNotFound()
    {
        // Arrange
        var productResult = Product.Create("Test Product", "Test Description", 99.99m);
        
        productResult.IsSuccess.Should().BeTrue();
        
        var product = productResult.Value;        

        // Act
        var result = await _repository.UpdateAsync(product);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(ProductErrors.NotFound);
    }

    [Fact]
    public async Task DeleteAsync_WithExistingId_ShouldReturnSuccess()
    {
        // Arrange
        var productResult = Product.Create("Test Product", "Test Description", 99.99m);
        productResult.IsSuccess.Should().BeTrue();
        var product = productResult.Value;
        await _repository.AddAsync(product);

        // Act
        var result = await _repository.DeleteAsync(product.Id);

        // Assert
        result.IsSuccess.Should().BeTrue();

        var retrievedResult = await _repository.GetByIdAsync(product.Id);

        retrievedResult.IsFailure.Should().BeTrue();
    }

    [Fact]
    public async Task DeleteAsync_WithNonExistentId_ShouldReturnNotFound()
    {
        // Arrange
        var nonExistentId = Guid.NewGuid();

        // Act
        var result = await _repository.DeleteAsync(nonExistentId);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(ProductErrors.NotFound);
    }

    [Fact]
    public async Task GetAllAsync_WithMultipleProducts_ShouldReturnAllProducts()
    {
        // Arrange
        var product1Result = Product.Create("Product 1", "Description 1", 10.00m);
        var product2Result = Product.Create("Product 2", "Description 2", 20.00m);
        var product3Result = Product.Create("Product 3", "Description 3", 30.00m);

        product1Result.IsSuccess.Should().BeTrue();
        product2Result.IsSuccess.Should().BeTrue();
        product3Result.IsSuccess.Should().BeTrue();

        await _repository.AddAsync(product1Result.Value);
        await _repository.AddAsync(product2Result.Value);
        await _repository.AddAsync(product3Result.Value);

        // Act
        var result = await _repository.GetAllAsync();

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().HaveCount(3);

        var productNames = result.Value.Select(p => p.Name).ToList();

        productNames.Should().Contain("Product 1");
        productNames.Should().Contain("Product 2");
        productNames.Should().Contain("Product 3");
        
        result.Value.Should().BeInDescendingOrder(p => p.CreatedAt);
    }

    [Fact]
    public async Task ExistsWithNameAsync_WithExistingName_ShouldReturnTrue()
    {
        // Arrange
        var productResult = Product.Create("Unique Product", "Description", 99.99m);
        productResult.IsSuccess.Should().BeTrue();
        await _repository.AddAsync(productResult.Value);

        // Act
        var result = await _repository.ExistsWithNameAsync("Unique Product");

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeTrue();
    }

    [Fact]
    public async Task ExistsWithNameAsync_WithNonExistentName_ShouldReturnFalse()
    {
        // Act
        var result = await _repository.ExistsWithNameAsync("Non-existent Product");

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeFalse();
    }

    [Fact]
    public async Task ExistsWithNameAsync_WithExcludeId_ShouldIgnoreExcludedProduct()
    {
        // Arrange
        var productResult = Product.Create("Test Product", "Description", 99.99m);
        productResult.IsSuccess.Should().BeTrue();
        var product = productResult.Value;
        await _repository.AddAsync(product);

        // Act
        var result = await _repository.ExistsWithNameAsync("Test Product", product.Id);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeFalse();
    }

    [Fact]
    public async Task ExistsWithNameAsync_CaseInsensitive_ShouldReturnTrue()
    {
        // Arrange
        var productResult = Product.Create("Test Product", "Description", 99.99m);
        productResult.IsSuccess.Should().BeTrue();
        await _repository.AddAsync(productResult.Value);

        // Act
        var result = await _repository.ExistsWithNameAsync("test product");

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeTrue();
    }
}
