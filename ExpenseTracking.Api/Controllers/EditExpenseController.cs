using ExpenseTracking.Domain.Services;
using ExpenseTracking.Shared.DataModels;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracking.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class EditExpenseController
{
    private readonly ExpenseService _service;

    public EditExpenseController(ExpenseService service)
    {
        _service = service;
    }

    [HttpGet(Name = "EditExpense")]
    public Expense Get(int id, double amount, string categoryName, string expenseDate, string description = "")
    {
        try
        {
            _service.EditExpense(id, amount, categoryName, expenseDate, description);
            return _service.GetExpense(id);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new Expense();
        }
    }
}