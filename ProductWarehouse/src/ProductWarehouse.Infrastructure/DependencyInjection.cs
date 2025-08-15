using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using ProductWarehouse.Core.Interfaces;
using ProductWarehouse.Infrastructure.Abstractions;
using ProductWarehouse.Infrastructure.Context;
using ProductWarehouse.Infrastructure.Repository;

namespace ProductWarehouse.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<ProductDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IDbConnectionFactory, DbConnectionFactory>();
        services.AddScoped<IProductRespository, ProductRepository>();

        return services;
    }
}
