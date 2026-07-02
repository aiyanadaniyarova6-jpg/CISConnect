using Microsoft.AspNetCore.Mvc;

namespace CISConnect.Controllers;

public class SavedController : Controller
{
    public IActionResult Index()
    {
        ViewData["Title"] = "Saved Posts";
        return View();
    }
}
