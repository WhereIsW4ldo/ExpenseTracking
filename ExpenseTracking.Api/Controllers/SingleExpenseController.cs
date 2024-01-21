using ExpenseTracking.Domain.Services;
using ExpenseTracking.Shared.DataModels;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracking.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class SingleExpenseController
{
    private readonly ExpenseService _service;

    public SingleExpenseController(ExpenseService service)
    {
        _service = service;
    }

    [HttpGet(Name = "GetExpense")]
    public Expense GetExpense(int id)
    {
        try
        {
            return _service.GetExpense(id);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new Expense();
        }
    }
}