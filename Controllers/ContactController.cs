using CISConnect.Services;
using CISConnect.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CISConnect.Controllers;

public class ContactController : Controller
{
    private readonly IEmailService _email;
    private readonly ILogger<ContactController> _logger;

    public ContactController(IEmailService email, ILogger<ContactController> logger)
    {
        _email = email;
        _logger = logger;
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
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to send contact form email");
            Console.Error.WriteLine($"EMAIL ERROR: {ex.GetType().Name}: {ex.Message}");
            TempData["ContactError"] = true;
        }

        return RedirectToAction("About", "Home");
    }
}
