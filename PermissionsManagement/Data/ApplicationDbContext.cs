using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PermissionsManagement.Models;

namespace PermissionsManagement.Data;
public class ApplicationDbContext : IdentityDbContext<AppUser
    ,AppRole,string,IdentityUserClaim<string>,IdentityUserRole<string>
    ,IdentityUserLogin<string>,AppRoleClaim,IdentityUserToken<string>>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
}
