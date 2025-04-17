namespace Blog.Models.Entities;

public class Post
{
    public int Id { get; set; }
    public string Title { get; set; } = default!;
    public string Content { get; set; } = default!;
    public DateTime CreatedAt { get; set; }
    public bool IsPublic { get; set; }
    public string Slug { get; set; } = default!;
    // Relationship with Category
    public Category Category { get; set; } = null!;
    public int CategoryId { get; set; }
    // Relationship with Tag
    public List<PostTag> PostTags { get; set; } = new List<PostTag>();
}
