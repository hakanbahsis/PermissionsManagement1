using Microsoft.AspNetCore.Identity;
using PermissionsManagement.Models;
using System.Reflection;
using System.Security.Claims;

namespace PermissionsManagement.Helpers;

public static class ClaimsHelper
{
    public static void GetPermissions(this List<RoleClaimsViewModel> allPermissions, Type policy, string roleId)
    {
        //FieldInfo[] fields = policy.GetFields(BindingFlags.Static | BindingFlags.Public);
        //foreach (FieldInfo fi in fields)
        //{
        //    allPermissions.Add(new RoleClaimsViewModel { Value = fi.GetValue(null).ToString(), Type = "Permissions" });
        //}

        var nestedTypes = policy.GetNestedTypes(BindingFlags.Static | BindingFlags.Public);
        foreach (var nestedType in nestedTypes)
        {
            var fields = nestedType.GetFields(BindingFlags.Static | BindingFlags.Public);
            foreach (var field in fields)
            {
                allPermissions.Add(new RoleClaimsViewModel { Value = field.GetValue(null).ToString(), Type = "Permissions" });
            }
        }

        if (roleId.Equals("SuperAdmin", StringComparison.OrdinalIgnoreCase))
        {
            foreach (var nestedType in nestedTypes)
            {
                var fields = nestedType.GetFields(BindingFlags.Static | BindingFlags.Public);
                foreach (var field in fields)
                {
                    if (!allPermissions.Any(p => p.Value == field.GetValue(null).ToString()))
                    {
                        allPermissions.Add(new RoleClaimsViewModel { Value = field.GetValue(null).ToString(), Type = "Permissions" });
                    }
                }
            }
        }
    }
    public static async Task AddPermissionsClaim(this RoleManager<AppRole> roleManager, AppRole role, string permission)
    {
        var allClaims = await roleManager.GetClaimsAsync(role);
        if (!allClaims.Any(a => a.Type == "Permission" && a.Value == permission))
        {
            await roleManager.AddClaimAsync(role, new Claim("Permission", permission));
        }
    }

    public static void GetAllPermissions(this List<RoleClaimsViewModel> allPermissions, Type policy, string roleId)
    {
        var nestedTypes = policy.GetNestedTypes(BindingFlags.Static | BindingFlags.Public);
        foreach (var nestedType in nestedTypes)
        {
            var fields = nestedType.GetFields(BindingFlags.Static | BindingFlags.Public);
            foreach (var field in fields)
            {
                allPermissions.Add(new RoleClaimsViewModel { Value = field.GetValue(null).ToString(), Type = "Permissions" });
            }
        }
    }
}
