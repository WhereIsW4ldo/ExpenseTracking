using ExpenseTracking.Shared.DAL;
using ExpenseTracking.Shared.DataModels;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace ExpenseTracking.Domain.Services;

public class ExpenseService
{
    private readonly ILogger _logger;
    private readonly ExpenseContext _context;

    public ExpenseService(ExpenseContext context)
    {
        _logger = new Logger<CategoryService>(new NullLoggerFactory());
        _context = context;
    }

    public Expense AddExpense(double amount, string description, int categoryId, string expenseDate)
    {
        var category = _context
            .Categories
            .FirstOrDefault(c => c.Id == categoryId);
        
        if (category is null)
        {
            throw new ArgumentException("Invalid category");
        }
        
        var expense = new Expense
        {
            Amount = amount,
            Description = description,
            Category = category,
            ExpenseDate = DateTime.Parse(expenseDate),
            CreationDate = DateTime.Now
        };

        _context.Add(expense);
        _context.SaveChanges();
        
        return expense;
    }
    
    public IEnumerable<Expense> GetExpenses()
    {
        var categories = _context
            .Categories
            .ToList();
        
        return _context
            .Expenses
            .ToList();
    }

    public IEnumerable<Expense> GetExpensesForCategory(int categoryId)
    {
        var categoryToFind = _context
            .Categories
            .FirstOrDefault(c => c.Id == categoryId);
        
        return _context
            .Expenses
            .Where(ex => ex.Category == categoryToFind)
            .ToList();
    }
    
    public Expense DeleteExpense(int id)
    {
        var categories = _context
            .Categories
            .ToList();
        
        var expenseToDelete = _context
            .Expenses
            .FirstOrDefault(ex => ex.ID == id);
        
        if (expenseToDelete is null)
        {
            throw new ArgumentException("Invalid expense");
        }
        
        _context.Remove(expenseToDelete);
        _context.SaveChanges();

        return expenseToDelete;
    }

    public Expense EditExpense(int id, double amount, string categoryName, string expenseDate, string description)
    {
        var expenseToEdit = _context
            .Expenses
            .FirstOrDefault(ex => ex.ID == id);
        
        var category = _context
            .Categories
            .FirstOrDefault(c => c.Name == categoryName);
        
        if (expenseToEdit is null || category is null)
        {
            throw new ArgumentException("Invalid expense or category");
        }
        
        expenseToEdit.Amount = amount;
        expenseToEdit.Category = category;
        expenseToEdit.ExpenseDate = DateTime.Parse(expenseDate);
        expenseToEdit.Description = description;

        _context.Update(expenseToEdit);
        _context.SaveChanges();
        return expenseToEdit;
    }
}