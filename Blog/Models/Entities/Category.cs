namespace Blog.Models.Entities;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    //Navigational prop
    public List<Post> Posts { get; set; } = new List<Post>();
}
