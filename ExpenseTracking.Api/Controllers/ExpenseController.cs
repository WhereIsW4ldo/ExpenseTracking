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

    [HttpGet]
    public IActionResult GetExpenses(int? category)
    {
        try
        {
            return category is null
                ? new OkObjectResult(_service.GetExpenses())
                : new OkObjectResult(_service.GetExpensesForCategory(category.Value));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new BadRequestObjectResult(e.Message);
        }
    }
    
    [HttpPut]
    public IActionResult PutExpenses(double amount, string description, int categoryId, string expenseDate)
    {
        try
        {
            var expense = _service.AddExpense(amount, description, categoryId, expenseDate);
            return new OkObjectResult(expense);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new BadRequestObjectResult(e.Message);
        }
    }
    
    [HttpDelete]
    public IActionResult DeleteExpenses(int id)
    {
        try
        {
            var removedExpense = _service.DeleteExpense(id);
            return new OkObjectResult(removedExpense);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new BadRequestObjectResult(e.Message);
        }
    }
}