using ExpenseTracking.Shared;
using ExpenseTracking.Shared.DataModels;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ExpenseTracking.Domain.Logic;

public class CategoryParser
{
    private readonly ILogger _logger;
    
    public CategoryParser(ILoggerFactory factory)
    {
        _logger = new Logger<CategoryParser>(factory);
    }

    public List<Category> GetCategories()
    {
        var json = File.ReadAllText(Constants.CategoriesJsonPath);
        var categories = JsonConvert.DeserializeObject<List<Category>>(json);

        return categories;
    }
}