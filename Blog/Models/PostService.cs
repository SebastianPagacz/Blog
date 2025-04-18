using System.Data;
using Blog.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
namespace Blog.Models;

public class PostService
{
    private readonly BloggingContext _context;
    public PostService(BloggingContext context)
    {
        _context = context;
    }
    
    // Create operations
    public async Task CreatePostAsync(string title, string content, string slug, bool isPublic, int categoryId)
    {
        var newPost = new Post
        {
            Title = title,
            Content = content,
            CreatedAt = DateTime.Now,
            Slug = slug,
            CategoryId = categoryId,
            IsPublic = isPublic
        };

        _context.Posts.Add(newPost);

        await _context.SaveChangesAsync();
    }
    
    // Read operations
    public async Task<List<Post>> GetAllPostsAsync()
    {
        return await _context.Posts
            .Where(p => p.IsPublic == true)
            .Include(p => p.Category)
            .ToListAsync();
    }

    public async Task<Post> GetPostById(int id)
    {
        var existingPost = await _context.Posts.FindAsync(id);

        if (existingPost != null && existingPost.IsPublic == true) 
        {
            return existingPost;
        }
        throw new MissingMemberException();
    }

    public async Task<List<Post>> GetPostsByCategory(int categoryId)
    {
        return await _context.Posts
            .Where(p => p.CategoryId == categoryId)
            .Where(p => p.IsPublic == true)
            .Include(p => p.Category)
            .OrderByDescending(p => p.CreatedAt)
            .ToListAsync();
    }
    // Update operations
    public async Task<bool> UpdatePostAsync(int id, string title, string content, string slug, int categoryId)
    {
        var existingPost = await _context.Posts.FindAsync(id);

        if (existingPost == null)
        {
            return false; // post not found
        }

        existingPost.Title = title;
        existingPost.Content = content;
        existingPost.Slug = slug;
        existingPost.CategoryId = categoryId;
        existingPost.IsPublic = true;

        _context.Entry(existingPost).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
            return true; // update successful
        }
        catch (DbUpdateConcurrencyException) // anoter user updated the same post at the same time
        {
            return false; // update failed
        }
    }

    public async Task<bool> DeletePostAsync(int id)
    {
        var exsitisingPost = await _context.Posts.FindAsync(id);
        if(exsitisingPost == null)
        {
            return false; // post not found
        }

        _context.Posts.Remove(exsitisingPost);

        await _context.SaveChangesAsync();
        return true; // operation successful
    }

    public async Task<bool> SoftDeletePostAsync(int id)
    {
        var exisitngPost = await _context.Posts.FindAsync(id);
        if(exisitngPost == null)
        {
            return false;
        }

        exisitngPost.IsPublic = false;

        _context.Entry(exisitngPost).State = EntityState.Modified;
        
        try
        {
            await _context.SaveChangesAsync();
            return true; // update successful
        }
        catch (DbUpdateConcurrencyException) // anoter user updated the same post at the same time
        {
            return false; // update failed
        }
    }

    public async Task<List<Post>> GetPostsByTitleKeywordAsync(string keyword)
    {
        return await _context.Posts
            .Where(p => p.Title.Contains(keyword))
            .OrderByDescending(p => p.CreatedAt)
            .Include(p => p.Category)
            .ToListAsync();
    }
}
