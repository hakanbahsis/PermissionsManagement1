namespace PermissionsManagement.Constants;

public class Permissions
{
    public static List<string> GeneratePermissionsForModule(string module)
    {
        return new List<string>
        {
            
            $"Permissions.{module}.Create",
            $"Permissions.{module}.View",
            $"Permissions.{module}.Edit",
            $"Permissions.{module}.Delete",
        };
    }

    public static class Test
    {
        
        public const string View = "Permissions.Test.View";
        public const string Create = "Permissions.Test.Create";
        public const string Edit = "Permissions.Test.Edit";
        public const string Delete = "Permissions.Test.Delete";
    }
    public static class Users
    {
        
        public const string View = "Permissions.Users.View";
        public const string Create = "Permissions.Users.Create";
        public const string Edit = "Permissions.Users.Edit";
        public const string Delete = "Permissions.Users.Delete";
    }
    public static class Roles
    {
        
        public const string View = "Permissions.Roles.View";
        public const string Create = "Permissions.Roles.Create";
        public const string Edit = "Permissions.Roles.Edit";
        public const string Delete = "Permissions.Roles.Delete";
    }
    public static class Permission
    {
        
        public const string View = "Permissions.Permissions.View";
        public const string Create = "Permissions.Permissions.Create";
        public const string Edit = "Permissions.Permissions.Edit";
        public const string Delete = "Permissions.RPermissionsoles.Delete";
    }
    public static class Products
    {
        
        public const string View = "Permissions.Products.View";
        public const string Create = "Permissions.Products.Create";
        public const string Edit = "Permissions.Products.Edit";
        public const string Delete = "Permissions.Products.Delete";
    }

    public static class Homes
    {
        
        public const string View = "Permissions.Home.View";
        public const string Create = "Permissions.Home.Create";
        public const string Edit = "Permissions.Home.Edit";
        public const string Delete = "Permissions.Home.Delete";
    }
    public static class Privacy
    {
        
        public const string View = "Permissions.Privacy.View";
        public const string Create = "Permissions.Privacy.Create";
        public const string Edit = "Permissions.Privacy.Edit";
        public const string Delete = "Permissions.Privacy.Delete";
    }
}
