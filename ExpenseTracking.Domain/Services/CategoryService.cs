using ExpenseTracking.Domain.Logic;
using ExpenseTracking.Shared.DataModels;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace ExpenseTracking.Domain.Services;

public class CategoryService
{
    private readonly ILogger _logger;
    private readonly CategoryParser _categoryParser;
    
    public CategoryService()
    {
        _logger = new Logger<CategoryService>(new NullLoggerFactory());
        _categoryParser = new CategoryParser(new NullLoggerFactory());
    }

    public IEnumerable<Category> GetCategories()
    {
        return _categoryParser.GetCategories();
    }
}