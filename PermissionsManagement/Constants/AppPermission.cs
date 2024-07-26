using System.Collections.ObjectModel;

namespace PermissionsManagement.Constants;

public record AppPermission(string Feature, string Action, string Group, string Description,bool IsBasic=false)
{
    public string Name { get; set; }
    public static string NameFor(string feature,string action)
    {
        return $"Permissions.{feature}.{action}";
    }
}

public class AppPermissions
{
    private static readonly AppPermission[] _all = new AppPermission[]
    {
        //Kullanıcı servis izinleri
       
        new (AppFeature.Users,AppAction.Create,AppRoleGroup.SystemAccess,"Create Users"),
        new (AppFeature.Users,AppAction.Update,AppRoleGroup.SystemAccess,"Update Users"),
        new (AppFeature.Users,AppAction.Read,AppRoleGroup.SystemAccess,"Read Users"),
        new (AppFeature.Users,AppAction.Delete,AppRoleGroup.SystemAccess,"Delete Users"),
        
        //Kullanıcı rol servis izinleri
        
        new (AppFeature.UserRoles,AppAction.Create,AppRoleGroup.SystemAccess,"Create Users Roles"),
        new (AppFeature.UserRoles,AppAction.Update,AppRoleGroup.SystemAccess,"Update User Roles"),
        new (AppFeature.UserRoles,AppAction.Read,AppRoleGroup.SystemAccess,"Read User Roles"),
        new (AppFeature.UserRoles,AppAction.Delete,AppRoleGroup.SystemAccess,"Delete User Roles"),
        
        //Rol servis izinleri
        
        new (AppFeature.Roles,AppAction.Create,AppRoleGroup.SystemAccess,"Create Roles"),
        new (AppFeature.Roles,AppAction.Update,AppRoleGroup.SystemAccess,"Update Roles"),
        new (AppFeature.Roles,AppAction.Read,AppRoleGroup.SystemAccess,"Read Roles"),
        new (AppFeature.Roles,AppAction.Delete,AppRoleGroup.SystemAccess,"Delete Roles"),
        
        //Rol Claim servis izinleri
        
        new (AppFeature.RoleClaims,AppAction.Update,AppRoleGroup.SystemAccess,"Update Role Claims/Permissions"),
        new (AppFeature.RoleClaims,AppAction.Read,AppRoleGroup.SystemAccess,"Read Role Claims/Permissions"),

        //Product servis izinleri
        
        new (AppFeature.Products,AppAction.Read,AppRoleGroup.Management,"Read Products",IsBasic:true),
        new (AppFeature.Products,AppAction.Create,AppRoleGroup.Management,"Create Products"),
        new (AppFeature.Products,AppAction.Update,AppRoleGroup.Management,"Update Products"),
        new (AppFeature.Products,AppAction.Delete,AppRoleGroup.Management,"Delete Products"),
        
       
    };

    public static IReadOnlyList<AppPermission> AdminPermissions { get; } =
        new ReadOnlyCollection<AppPermission>(_all.Where(p => !p.IsBasic).ToArray());

    public static IReadOnlyList<AppPermission> BasicPermissions { get; } =
        new ReadOnlyCollection<AppPermission>(_all.Where(p => p.IsBasic).ToArray());

    public static IReadOnlyList<AppPermission> AllPermissions { get; }
        = new ReadOnlyCollection<AppPermission>(_all);
};

