using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using ProductWarehouse.Application.Interfaces;
using ProductWarehouse.Application.Services;
using ProductWarehouse.Infrastructure;

namespace ProductWarehouse.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(
    this IServiceCollection services,
    IConfiguration configuration)
    {
        services.AddInfrastructure(configuration);

        services.AddScoped<IProductService, ProductService>();

        return services;
    }
}
