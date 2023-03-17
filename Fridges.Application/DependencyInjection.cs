using Fridges.Application.Services.Implementations;
using Fridges.Application.Services.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Fridges.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddAplicaton(this IServiceCollection services)
    {
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IFridgeService, FridgeService>();

        return services;
    }
}
