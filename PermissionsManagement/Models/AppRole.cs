using Microsoft.AspNetCore.Identity;

namespace PermissionsManagement.Models;

public class AppRole:IdentityRole<string>
{
    public string Description { get; set; }
}
