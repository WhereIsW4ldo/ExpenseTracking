using ExpenseTracking.Shared.DAL;
using ExpenseTracking.Shared.DataModels;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace ExpenseTracking.Domain.Services;

public class TransactionService
{
    private readonly ILogger _logger;
    private readonly ExpenseContext _context;

    public TransactionService(ExpenseContext context)
    {
        _logger = new Logger<CategoryService>(new NullLoggerFactory());
        _context = context;
    }

    public Expense AddTransaction(double amount, string description, int categoryId, string expenseDate)
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
            ExpenseDate = DateTime.Parse(expenseDate).ToUniversalTime(),
            CreationDate = DateTime.Now.ToUniversalTime()
        };

        _context.Expenses.Add(expense);
        _context.SaveChanges();
        
        return expense;
    }
    
    public IEnumerable<Expense> GetTransactions()
    {
        var categories = _context
            .Categories
            .ToList();
        
        return _context
            .Expenses
            .ToList();
    }

    public IEnumerable<Expense> GetTransactionsForCategory(int categoryId)
    {
        var categoryToFind = _context
            .Categories
            .FirstOrDefault(c => c.Id == categoryId);
        
        return _context
            .Expenses
            .Where(ex => ex.Category == categoryToFind)
            .ToList();
    }
    
    public Expense DeleteTransaction(int id)
    {
       var expenseToDelete = _context
            .Expenses
            .FirstOrDefault(ex => ex.Id == id);
        
        if (expenseToDelete is null)
        {
            throw new ArgumentException("Invalid expense");
        }
        
        _context.Expenses.Remove(expenseToDelete);
        _context.SaveChanges();

        return expenseToDelete;
    }

    public Expense UpdateTransaction(int id, double amount, string description, int categoryId, string expenseDate)
    {
        var expenseToEdit = _context
            .Expenses
            .Find(id);

        var category = _context
            .Categories
            .Find(categoryId);
        
        if (expenseToEdit is null || category is null)
        {
            throw new ArgumentException("Invalid expense or category");
        }
        
        expenseToEdit.Amount = amount;
        expenseToEdit.Category = category;
        expenseToEdit.ExpenseDate = DateTime.Parse(expenseDate).ToUniversalTime();
        expenseToEdit.Description = description;

        _context.Update(expenseToEdit);
        _context.SaveChanges();
        return expenseToEdit;
    }
}