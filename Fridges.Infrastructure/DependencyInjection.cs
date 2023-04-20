using Fridges.Application.Repositories;
using Fridges.Infrastructure.Data;
using Fridges.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Fridges.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Default");
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(connectionString, o => o.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName));
        });

        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IFridgeRepository, FridgeRepository>();
        services.AddScoped<IFridgeModelRepository, FridgeModelRepository>();
        services.AddScoped<IFridgeProductRepository, FridgeProductRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();

        return services;
    }
}
