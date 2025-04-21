using System.Diagnostics;
using Blog.Models;
using Blog.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers;

public class AdminController : Controller
{

    private readonly PostService _postService;
    private readonly CategoryService _categoryService;
    private readonly ILogger<AdminController> _logger;

    public AdminController(ILogger<AdminController> logger, PostService postService, CategoryService categoryService)
    {
        _logger = logger;
        _postService = postService;
        _categoryService = categoryService;
    }
    public IActionResult AdminIndex()
    {
        return View();
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
            return RedirectToAction("AdminIndex", "Admin");
        }

        var categories = await _categoryService.GetAllCategoriesAsync();
        model.Categories = categories;
        return View(model);
    }


    [HttpGet]
    public async Task<IActionResult> Delete()
    {
        return View(await _postService.GetNonPublicPostsAsync());
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int postId)
    {
        await _postService.DeletePostAsync(postId);
        return RedirectToAction("AdminIndex", "Admin");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }


}
