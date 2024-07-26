using Microsoft.AspNetCore.Identity;
using PermissionsManagement.Constants;
using PermissionsManagement.Models;

namespace PermissionsManagement.Seeds;

public static class DefaultRoles
{
    public static async Task SeedAsync(UserManager<AppUser> userManager,RoleManager<AppRole> roleManager)
    {
        //await roleManager.CreateAsync(new AppRole(Roles.SuperAdmin.ToString()));
        //await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
        //await roleManager.CreateAsync(new IdentityRole(Roles.Basic.ToString()));
    }
}
