using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Blog.Models;
using Blog.Models.ViewModels;

namespace Blog.Controllers;

public class HomeController : Controller
{
    private readonly PostService _postService;
    private readonly CategoryService _categoryService;
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger, PostService postService, CategoryService categoryService)
    {
        _logger = logger;
        _postService = postService;
        _categoryService = categoryService;
    }

    public IActionResult Index()
    {
        return View();
    }

    public async Task<IActionResult> Posts()
    {
        var result = await _postService.GetAllPostsAsync();
        return View(result);
    }
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }
    [HttpPost]
    public IActionResult Login(LoginViewModel model)
    {
        return RedirectToAction("AdminIndex", "Admin");
    }
    [HttpGet]
    public IActionResult LogOut() 
    {
        return RedirectToAction("Index", "Home");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
