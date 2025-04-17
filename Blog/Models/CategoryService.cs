using Blog.Models.Entities;
using Microsoft.EntityFrameworkCore;
namespace Blog.Models;

public class CategoryService
{
    private readonly BloggingContext _context;
    public CategoryService(BloggingContext context)
    {
        _context = context;
    }

    public async Task AddCategoryAsync(string name, string desccription)
    {
        var newCategory = new Category
        {
            Name = name,
            Description = desccription
        };

        _context.Categories.Add(newCategory);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Category>> GetAllCategoriesAsync()
    {
        return await _context.Categories.ToListAsync();
    }

    public async Task<bool> UpdateCategoryAsync(int id, string name, string description)
    {
        var exisitngCategory = await _context.Categories.FindAsync(id);

        if (exisitngCategory == null) 
        {
            return false;
        }

        exisitngCategory.Name = name;
        exisitngCategory.Description = description;
       
        _context.Entry(exisitngCategory).State = EntityState.Modified;
        try
        {
            await _context.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateConcurrencyException) 
        {
            return false;
        }
        

    }
}
