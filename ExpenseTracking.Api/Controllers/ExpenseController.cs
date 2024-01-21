using ExpenseTracking.Domain.Services;
using ExpenseTracking.Shared.DataModels;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracking.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ExpenseController
{
    private readonly ExpenseService _service;

    public ExpenseController(ExpenseService service)
    {
        _service = service;
    }

    [HttpGet(Name = "GetExpenses")]
    public IEnumerable<Expense> GetExpenses(string category = "")
    {
        try
        {
            return string.IsNullOrWhiteSpace(category)
                ? _service.GetExpenses()
                : _service.GetExpensesForCategory(category);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new List<Expense>();
        }
    }
}