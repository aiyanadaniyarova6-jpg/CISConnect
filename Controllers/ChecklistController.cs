using Microsoft.AspNetCore.Mvc;

namespace CISConnect.Controllers;

public class ChecklistController : Controller
{
    public IActionResult Index()
    {
        ViewData["Title"] = "My Checklist";
        return View();
    }
}
