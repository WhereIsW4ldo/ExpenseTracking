using ExpenseTracking.Shared;
using ExpenseTracking.Shared.DataModels;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ExpenseTracking.Domain.Logic;

public class ExpenseParser
{
    private readonly ILogger _logger;
    
    public ExpenseParser(ILoggerFactory factory)
    {
        _logger = new Logger<CategoryParser>(factory);
    }

    public List<Expense> GetExpenses()
    {
        var json = File.ReadAllText(Constants.ExpensesJsonPath);
        var expenses = JsonConvert.DeserializeObject<List<Expense>>(json);

        return expenses;
    }
}