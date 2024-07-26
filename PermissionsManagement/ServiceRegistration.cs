using Microsoft.AspNetCore.Identity;
using PermissionsManagement.Data;
using PermissionsManagement.Models;

namespace PermissionsManagement;

public static class ServiceRegistration
{
    internal static IApplicationBuilder SeedDatabase(this IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.CreateScope();
        var seeders = serviceScope.ServiceProvider.GetServices<ApplicationDbSeeder>();

        foreach (var seeder in seeders)
        {
            seeder.SeedDatabaseAsync().GetAwaiter().GetResult();
        }

        return app;
    }

    internal static IServiceCollection AddIdentitySettings(this IServiceCollection services)
    {
        services
            .AddIdentity<AppUser, AppRole>(opt =>
            {
                opt.Password.RequiredLength = 6;
                opt.Password.RequireDigit = false;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireNonAlphanumeric = false;
                opt.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultUI()
            .AddDefaultTokenProviders();

        return services;
    }
}
