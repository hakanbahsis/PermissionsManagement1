using Microsoft.AspNetCore.Identity;

namespace PermissionsManagement.Models;

public class AppUser:IdentityUser<string>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public bool IsActive { get; set; }
}
