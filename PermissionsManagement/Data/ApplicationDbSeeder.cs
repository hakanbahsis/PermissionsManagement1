using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PermissionsManagement.Constants;
using PermissionsManagement.Models;

namespace PermissionsManagement.Data;

public class ApplicationDbSeeder
{
    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<AppRole> _roleManager;
    private readonly ApplicationDbContext _dbContext;
    public ApplicationDbSeeder(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, ApplicationDbContext dbContext)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _dbContext = dbContext;
    }

    public async Task SeedDatabaseAsync()
    {
        //check for pending and apply if any
        await CheckAndApplyPendingMigration();
        //seed roles
        await SeedRolesAsync();
        //seed user(basic)
        await SeedBasicUserAsync();
        //seed user(admin)
        await SeedAdminUserAsync();
    }

    private async Task SeedBasicUserAsync()
    {
        var basicUser = new AppUser
        {
            Id=Guid.NewGuid().ToString(),
            FirstName = "John",
            LastName = "Doe",
            Email = "johnd@abc.com",
            NormalizedEmail = "JOHND@ABC.COM",
            UserName = "johnd",
            NormalizedUserName = "JOHND",
            EmailConfirmed = true,
            PhoneNumberConfirmed = true,
            IsActive = true
        };
        if (!await _userManager.Users.AnyAsync(u => u.Email == "johnd@abc.com"))
        {
            var password = new PasswordHasher<AppUser>();
            basicUser.PasswordHash = password.HashPassword(basicUser, AppCredentials.Password);
            await _userManager.CreateAsync(basicUser);
        }
        //Assign role to user
        if (!await _userManager.IsInRoleAsync(basicUser, AppRoles.Basic))
        {
            await _userManager.AddToRoleAsync(basicUser, AppRoles.Basic);
        }
    }
    private async Task SeedAdminUserAsync()
    {
        string adminUserName = AppCredentials.Email[..AppCredentials.Email.IndexOf('@')].ToLowerInvariant();
        var adminUser = new AppUser
        {
            Id = Guid.NewGuid().ToString(),
            FirstName = "Hakan",
            LastName = "Bahşiş",
            Email = AppCredentials.Email,
            UserName = adminUserName,
            EmailConfirmed = true,
            PhoneNumberConfirmed = true,
            NormalizedEmail = AppCredentials.Email.ToUpperInvariant(),
            NormalizedUserName = adminUserName.ToUpperInvariant(),
            IsActive = true
        };

        if (!await _userManager.Users.AnyAsync(u => u.Email == AppCredentials.Email))
        {
            var password = new PasswordHasher<AppUser>();
            adminUser.PasswordHash = password.HashPassword(adminUser, AppCredentials.Password);
            await _userManager.CreateAsync(adminUser);
        }

        // Assign role to user
        if (!await _userManager.IsInRoleAsync(adminUser, AppRoles.Basic)
            && !await _userManager.IsInRoleAsync(adminUser, AppRoles.Admin))
        {
            await _userManager.AddToRolesAsync(adminUser, AppRoles.DefaultRoles);
        }
    }

    private async Task CheckAndApplyPendingMigration()
    {
        if(_dbContext.Database.GetPendingMigrations().Any())
        {
            await _dbContext.Database.MigrateAsync();
        }
    }

    private async Task SeedRolesAsync()
    {
        foreach (var roleName in AppRoles.DefaultRoles)
        {
            if (await _roleManager.Roles.FirstOrDefaultAsync(r => r.Name == roleName)
                is not AppRole role)
            {
                role = new AppRole
                {
                    Id=Guid.NewGuid().ToString(),
                    Name = roleName,
                    Description = $"{roleName} Role."
                };
                await _roleManager.CreateAsync(role);
            }

            //Assign Permissions
            if (roleName == AppRoles.Admin)
            {
                //Admin
                await AssignPermissionsToRoleAsync(role, AppPermissions.AdminPermissions);
            }
            else if (roleName == AppRoles.Basic)
            {
                //Basic
                await AssignPermissionsToRoleAsync(role, AppPermissions.BasicPermissions);
            }
        }
    }

    private async Task AssignPermissionsToRoleAsync(AppRole role, IReadOnlyList<AppPermission> permissions)
    {
        // Role null ise, ArgumentNullException fırlat
        if (role == null) throw new ArgumentNullException(nameof(role));
        // Permissions null ise, ArgumentNullException fırlat
        if (permissions == null) throw new ArgumentNullException(nameof(permissions));

        var currentClaims = await _roleManager.GetClaimsAsync(role);
        foreach (var permission in permissions)
        {
            if (!currentClaims.Any(claim => claim.Type == AppClaim.Permission && claim.Value == permission.Name))
            {
                await _dbContext.RoleClaims.AddAsync(new AppRoleClaim
                {
                    RoleId = role.Id,
                    ClaimType = AppClaim.Permission,
                    ClaimValue = permission.Name,
                    Description = permission.Description,
                    Group = permission.Group
                });
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
