using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Udemy.Identity.Controllers;

[Authorize(Roles = "Admin")]
public class UserController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}