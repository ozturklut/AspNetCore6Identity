using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Udemy.Identity.Context;
using Udemy.Identity.Entities;
using Udemy.Identity.Models;

namespace Udemy.Identity.Controllers;

[Authorize(Roles = "Admin")]
public class UserController : Controller
{
    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<AppRole> _roleManager;
    private readonly UdemyContext _context;

    public UserController(UserManager<AppUser> userManager, UdemyContext context, RoleManager<AppRole> roleManager)
    {
        _userManager = userManager;
        _context = context;
        _roleManager = roleManager;
    }

    public IActionResult Index()
    {
        var query = _userManager.Users;
        var users = _context.Users.Join(_context.UserRoles, user => user.Id, userRole => userRole.UserId, (user, userRole) => new
        {
            user,
            userRole
        }).Join(_context.Roles, two => two.userRole.RoleId, role => role.Id, (two, role) => new { two.user, two.userRole, role }).Where(x => x.role.Name != "Admin").Select(x => new AppUser
        {
            Id = x.user.Id,
            AccessFailedCount = x.user.AccessFailedCount,
            ConcurrencyStamp = x.user.ConcurrencyStamp,
            Email = x.user.Email,
            EmailConfirmed = x.user.EmailConfirmed,
            Gender = x.user.Gender,
            ImagePath = x.user.ImagePath,
            LockoutEnabled = x.user.LockoutEnabled,
            LockoutEnd = x.user.LockoutEnd,
            NormalizedEmail = x.user.NormalizedEmail,
            NormalizedUserName = x.user.NormalizedUserName,
            PasswordHash = x.user.PasswordHash,
            PhoneNumber = x.user.PhoneNumber,
            UserName = x.user.UserName,
        }).ToList();



        return View(users);
    }

    public IActionResult Create()
    {
        return View(new UserAdminCreateModel());
    }

    [HttpPost]
    public async Task<IActionResult> Create(UserAdminCreateModel model)
    {
        if (ModelState.IsValid)
        {
            var user = new AppUser
            {
                Email = model.Email,
                Gender = model.Gender,
                UserName = model.UserName,
            };
            var result = await _userManager.CreateAsync(user, model.UserName + "123.");
            if (result.Succeeded)
            {
                var memberRole = await _roleManager.FindByNameAsync("Member");
                if (memberRole == null)
                {
                    await _roleManager.CreateAsync(new()
                    {
                        Name = "Member",
                        CreatedTime = DateTime.Now,
                    });
                }

                await _userManager.AddToRoleAsync(user, "Member");
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