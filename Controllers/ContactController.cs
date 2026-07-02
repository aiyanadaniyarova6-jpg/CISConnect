using CISConnect.Services;
using CISConnect.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CISConnect.Controllers;

public class ContactController : Controller
{
    private readonly IEmailService _email;

    public ContactController(IEmailService email)
    {
        _email = email;
    }

    [HttpGet]
    public IActionResult Index()
    {
        ViewData["Title"] = "Contact Us";
        return View(new ContactViewModel());
    }

    // POST: redirect instead of returning a view so the success/error state
    // survives a page refresh without resubmitting the form (PRG pattern).
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Index(ContactViewModel model)
    {
        if (!ModelState.IsValid)
        {
            TempData["ContactError"] = true;
            return RedirectToAction("About", "Home");
        }

        try
        {
            await _email.SendContactFormAsync(model.Name, model.Email, model.Subject, model.Message);
            TempData["ContactSent"] = true;
        }
        catch
        {
            TempData["ContactError"] = true;
        }

        return RedirectToAction("About", "Home");
    }
}
