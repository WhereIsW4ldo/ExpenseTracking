using ExpenseTracking.Shared.DAL;
using ExpenseTracking.Shared.DataModels;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace ExpenseTracking.Domain.Services;

public class CategoryService
{
    private readonly ILogger _logger;
    private readonly ExpenseContext _context;
    
    public CategoryService(ExpenseContext context)
    {
        _logger = new Logger<CategoryService>(new NullLoggerFactory());
        _context = context;
    }

    public IEnumerable<Category> GetCategories()
    {
        return _context
            .Categories
            .ToList();
    }
    
    public Category AddCategory(string name)
    {
        var category = new Category
        {
            Name = name
        };
        _context.Categories.Add(category);
        _context.SaveChanges();

        return category;
    }

    public Category RemoveCategory(int id)
    {
        var category = _context.Categories.Find(id);
        if (category != null)
        {
            _context.Categories.Remove(category);
            _context.SaveChanges();
            return category;
        }
        throw new ArgumentException("Category not found");
    }
    
    public Category UpdateCategory(int id, string name)
    {
        var category = _context.Categories.Find(id);
        if (category != null)
        {
            category.Name = name;
            _context.SaveChanges();
            return category;
        }
        throw new ArgumentException("Category not found");
    }
}