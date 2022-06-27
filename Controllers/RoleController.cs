using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using IdentityApp.Entities;
using IdentityApp.Models;

namespace IdentityApp.Controllers;

[Authorize(Roles = "Admin")]
public class RoleController : Controller
{
    private readonly RoleManager<AppRole> _roleManager;

    public RoleController(RoleManager<AppRole> roleManager)
    {
        _roleManager = roleManager;
    }

    public IActionResult Index()
    {
        var roleList = _roleManager.Roles.ToList();
        return View(roleList);
    }

    public IActionResult Create()
    {
        return View(new RoleCreateModel());
    }

    [HttpPost]
    public async Task<IActionResult> Create(RoleCreateModel model)
    {
        if (ModelState.IsValid)
        {
            var result = await _roleManager.CreateAsync(new AppRole
            {
                CreatedTime = DateTime.Now,
                Name = model.Name
            });
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            foreach (var item in result.Errors)
            {
                ModelState.AddModelError("", item.Description);
            }
        }


        return View(model);
    }
}