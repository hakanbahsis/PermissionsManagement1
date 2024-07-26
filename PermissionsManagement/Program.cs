using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PermissionsManagement;
using PermissionsManagement.Constants;
using PermissionsManagement.Data;
using PermissionsManagement.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped<UserManager<AppUser>>();
builder.Services.AddScoped<RoleManager<AppRole>>();

//builder.Services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
//builder.Services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();


var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") 
                                                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(connectionString)).AddTransient<ApplicationDbSeeder>();
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentitySettings();

builder.Services.AddAuthorization(options =>
{
    foreach (var prop in typeof(AppPermissions).GetNestedTypes().SelectMany(c => c.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)))
    {
        var propertyValue = prop.GetValue(null);
        if (propertyValue is not null)
        {
            options.AddPolicy(propertyValue.ToString(), policy => policy.RequireClaim(AppClaim.Permission, propertyValue.ToString()));
        }
    }
});
//builder.Services.AddSingleton<IAuthorizationPolicyProvider,PermissionPolicyProvider>()
//    .AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>()
//    .AddIdentity<AppUser, AppRole>(opt =>
//    {
//        opt.Password.RequireNonAlphanumeric = false;
//        opt.Password.RequireLowercase = false;
//        opt.Password.RequireUppercase = false;
//    }).AddRoleManager<RoleManager<AppRole>>()
//    .AddEntityFrameworkStores<ApplicationDbContext>()
//    .AddDefaultUI()
//    .AddDefaultTokenProviders();

builder.Services.AddControllersWithViews();



var app = builder.Build();
//app.SeedDatabase();





//using (var scope = app.Services.CreateScope())
//{
//    var services = scope.ServiceProvider;
//    var loggerFactory = services.GetRequiredService<ILoggerFactory>();
//    var logger = loggerFactory.CreateLogger("app");
//    try
//    {
//        var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
//        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();


//        await DefaultRoles.SeedAsync(userManager, roleManager);
//        await DefaultUsers.SeedTestUserAsync(userManager, roleManager);
//        await DefaultUsers.SeedBasicUserAsync(userManager, roleManager);
//        logger.LogInformation("Seeding admin Data");
//        await DefaultUsers.SeedAdminUserAsync(userManager, roleManager);
//        logger.LogInformation("finish admin Data");
//        logger.LogInformation("seedin superadmin Data");
//        await DefaultUsers.SeedSuperAdminUserAsync(userManager, roleManager);
//        logger.LogInformation("finish superadmin Data");
//        logger.LogInformation("Finished Seeding Default Data");
//        logger.LogInformation("Application Starting");
//    }
//    catch (Exception ex)
//    {
//        logger.LogWarning(ex, "An error occurred seeding the DB");
//    }


//}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); 
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();




app.Run();
