using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using LinuxDeployment.Models;

namespace LinuxDeployment.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        _logger.LogInformation("Index page loaded");
        return View();
    }

    public IActionResult Privacy()
    {
        _logger.LogInformation("Privacy page loaded");
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        var requestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;

        _logger.LogError("Error page shown. RequestId: {RequestId}", requestId);

        return View(new ErrorViewModel { RequestId = requestId });
    }
}