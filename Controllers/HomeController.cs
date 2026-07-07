using System.Diagnostics;
using CISConnect.Models;
using Microsoft.AspNetCore.Mvc;

namespace CISConnect.Controllers;

public class HomeController : Controller
{
    public IActionResult About()
    {
        return View();
    }

    // TEMP: throws on purpose to verify Sentry error tracking in production. Remove after testing.
    public IActionResult SentryTest()
    {
        throw new Exception("Sentry test error — verifying production error tracking.");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error(int? statusCode = null)
    {
        if (statusCode.HasValue)
        {
            ViewData["StatusCode"] = statusCode.Value;
            return View("StatusCode");
        }
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
