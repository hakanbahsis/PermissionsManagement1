﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PermissionsManagement.Models;

namespace PermissionsManagement.Controllers;
[Authorize(Roles ="SuperAdmin")]
public class RolesController : Controller
{
    private readonly RoleManager<AppRole> _roleManager;

    public RolesController(RoleManager<AppRole> roleManager)
    {
        _roleManager = roleManager;
    }

    public async Task<IActionResult> Index()
    {
        var roles = await _roleManager.Roles.ToListAsync();
        return View(roles);
    }
    [HttpPost]
    public async Task<IActionResult> AddRole(string roleName)
    {
        if (roleName is not null)
        {
          //  await _roleManager.CreateAsync(new AppRole(roleName));
        }
        return RedirectToAction("Index");
    }
}
