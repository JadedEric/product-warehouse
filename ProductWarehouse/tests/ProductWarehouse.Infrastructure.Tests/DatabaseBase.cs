
using Microsoft.EntityFrameworkCore;

using ProductWarehouse.Core.Interfaces;
using ProductWarehouse.Infrastructure.Context;

using Testcontainers.PostgreSql;

namespace ProductWarehouse.Infrastructure.Tests;

public abstract class DatabaseTestBase : IAsyncLifetime
{
    private readonly PostgreSqlContainer _postgresContainer = new PostgreSqlBuilder()
        .WithDatabase("testdb")
        .WithUsername("testuser")
        .WithPassword("testpass")
        .WithCleanUp(true)
        .Build();

    protected ProductDbContext DbContext { get; private set; } = null!;
    
    protected IDbConnectionFactory ConnectionFactory { get; private set; } = null!;

    public virtual async Task InitializeAsync()
    {
        await _postgresContainer.StartAsync();

        var connectionString = _postgresContainer.GetConnectionString();
                
        var options = new DbContextOptionsBuilder<ProductDbContext>()
            .UseNpgsql(connectionString)
            .Options;

        DbContext = new ProductDbContext(options);
        
        await DbContext.Database.EnsureCreatedAsync();
        
        ConnectionFactory = new TestDbConnectionFactory(connectionString);
    }

    public async Task DisposeAsync()
    {
        await DbContext.DisposeAsync();
        await _postgresContainer.DisposeAsync();
    }
}
