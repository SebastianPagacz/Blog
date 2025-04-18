using Blog.Models.Entities;
namespace Blog.Models.ViewModels;

public class CreatePostViewModel
{
    public string Title { get; set; } = default!;
    public string Content { get; set; } = default!;
    public string Slug { get; set; } = default!;
    public bool IsPublic { get; set; }
    public int CategoryId { get; set; }
    public List<Category> Categories { get; set; } = new List<Category>();
}
