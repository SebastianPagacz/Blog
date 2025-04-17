using Blog.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Blog.Models;

public class BloggingContext : DbContext
{
    public BloggingContext(DbContextOptions<BloggingContext> options) : base(options) { }

    public DbSet<Post> Posts { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<PostTag> PostTags { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Post>()
            .HasOne(p => p.Category) // p (Post) has only one Category
            .WithMany(c => c.Posts) // c (Category) has many Posts
            .HasForeignKey(p => p.CategoryId) // p (Post) has foreign key referencing to Category
            .OnDelete(DeleteBehavior.Cascade);

        // Declaring Primary Keys for PostTag table
        modelBuilder.Entity<PostTag>()
            .HasKey(pt => new { pt.PostId, pt.TagId });

        modelBuilder.Entity<PostTag>()
            .HasOne(pt => pt.Post)
            .WithMany(p => p.PostTags)
            .HasForeignKey(pt => pt.PostId);

        modelBuilder.Entity<PostTag>()
            .HasOne(pt => pt.Tag)
            .WithMany(t => t.PostTags)
            .HasForeignKey(pt => pt.TagId);
    }

    public List<Post> GetAllPosts()
    {
        return Posts
            .ToList();
    }

    public List<Post> GetAllPublicPosts()
    {
        return Posts
            .Where(p => p.IsPublic == true)
            .ToList();
    }
}
