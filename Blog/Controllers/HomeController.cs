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

    public IActionResult Privacy()
    {
        return View();
    }

    public async Task<IActionResult> Posts()
    {
        var result = await _postService.GetAllPostsAsync();
        return View(result);
    }
    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var categories = await _categoryService.GetAllCategoriesAsync();
        var viewModel = new CreatePostViewModel { Categories = categories };
        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreatePostViewModel model) 
    {
        if (ModelState.IsValid) 
        {
            await _postService.CreatePostAsync(model.Title, model.Content, model.Slug, model.IsPublic, model.CategoryId);
            return RedirectToAction("Posts", "Home");
        }

        var categories = await _categoryService.GetAllCategoriesAsync();
        model.Categories = categories;
        return View(model);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
