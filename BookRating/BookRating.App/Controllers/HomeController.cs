using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BookRating.App.Models;
using BookRating.App.Services;

namespace BookRating.App.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    private readonly MyService _myService;

    public HomeController(ILogger<HomeController> logger, MyService myService)
    {
        _logger = logger;
        _myService = myService;
    }

    public IActionResult Index()
    {
        _myService.Print("my service works");
        return View(new IndexViewModel { abc = "My val" });
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

