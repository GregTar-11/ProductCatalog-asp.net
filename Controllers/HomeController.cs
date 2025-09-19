using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Models;
using ProductCatalog.Services; 

namespace ProductCatalog.Controllers;

public class HomeController : Controller
{
     private readonly IGreetingService _greetingService;
    private readonly ILogger<HomeController> _logger;

    // DI внедрит GreetingService через конструктор
    public HomeController(IGreetingService greetingService, ILogger<HomeController> logger)
        {
            _greetingService = greetingService;
            _logger = logger;
        }

        public IActionResult Index()
        {
            ViewBag.Message = _greetingService.GetMessage();
            return View();
        }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
