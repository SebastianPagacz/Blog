using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Blog.Models;

namespace Blog.Controllers;

public class HomeController : Controller
{
    private readonly BloggingContext _context;
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger, BloggingContext dbContext)
    {
        _logger = logger;
        _context = dbContext;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult Posts()
    {
        var result = _context.GetAllPosts();
        return View(result);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
