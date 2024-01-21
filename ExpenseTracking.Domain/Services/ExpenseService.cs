using ExpenseTracking.Domain.Logic;
using ExpenseTracking.Shared;
using ExpenseTracking.Shared.DataModels;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Newtonsoft.Json;

namespace ExpenseTracking.Domain.Services;

public class ExpenseService
{
    private readonly ILogger _logger;
    private readonly ExpenseParser _expenseParser;
    
    public ExpenseService()
    {
        _logger = new Logger<CategoryService>(new NullLoggerFactory());
        _expenseParser = new ExpenseParser(new NullLoggerFactory());
    }

    private void SaveExpenses(IEnumerable<Expense> expenses)
    {
        var serializeObject = JsonConvert.SerializeObject(expenses, Formatting.Indented);
        File.WriteAllText(Constants.ExpensesJsonPath, serializeObject);
    }

    public IEnumerable<Expense> GetExpenses()
    {
        return _expenseParser.GetExpenses();
    }

    public IEnumerable<Expense> GetExpensesForCategory(string categoryName)
    {
        return _expenseParser.GetExpenses().Where(ex => ex.CategoryName == categoryName);
    }

    public Expense GetExpense(int id)
    {
        return _expenseParser.GetExpenses().First(ex => ex.ID == id);
    }

    public void EditExpense(int id, double amount, string categoryName, string expenseDate, string description)
    {
        var expenses = GetExpenses().ToList();

        var expenseToEdit = expenses.FirstOrDefault(ex => ex.ID == id);

        var expenseRemoved = expenses.Remove(expenseToEdit);

        if (!expenseRemoved) return;
        
        var newExpense = new Expense
        {
            ID = id,
            Amount = amount,
            Description = description,
            CategoryName = categoryName,
            ExpenseDate = DateTime.Parse(expenseDate),
            CreationDate = expenseToEdit.CreationDate
        };
        expenses.Add(newExpense);
        SaveExpenses(expenses);
    }
}