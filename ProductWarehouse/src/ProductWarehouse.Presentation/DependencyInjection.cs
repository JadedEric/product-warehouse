using FastEndpoints;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using ProductWarehouse.Application;

using System.Text.Json;

namespace ProductWarehouse.Presentation;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(
            this IServiceCollection services,
            IConfiguration configuration)
    {
        services.AddApplication(configuration);

        // Add FastEndpoints
        services.AddFastEndpoints();

        return services;
    }

    public static WebApplication UsePresentation(this WebApplication app)
    {
        // Configure FastEndpoints
        app.UseFastEndpoints(c =>
        {
            c.Endpoints.RoutePrefix = "api";
            c.Serializer.Options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        });

        return app;
    }
}
