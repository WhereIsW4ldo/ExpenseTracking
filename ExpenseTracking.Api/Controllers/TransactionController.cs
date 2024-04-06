using ExpenseTracking.Domain.Services;
using ExpenseTracking.Shared.DataModels;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracking.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class TransactionController
{
    private readonly TransactionService _service;

    public TransactionController(TransactionService service)
    {
        _service = service;
    }

    [HttpGet]
    public IActionResult GetExpenses(int? category)
    {
        try
        {
            return category is null
                ? new OkObjectResult(_service.GetTransactions())
                : new OkObjectResult(_service.GetTransactionsForCategory(category.Value));
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
            var expense = _service.AddTransaction(amount, description, categoryId, expenseDate);
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
            var removedExpense = _service.DeleteTransaction(id);
            return new OkObjectResult(removedExpense);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new BadRequestObjectResult(e.Message);
        }
    }
    
    [HttpPost]
    public IActionResult UpdateTransaction(int id, double amount, string description, int categoryId, string expenseDate)
    {
        try
        {
            var updatedExpense = _service.UpdateTransaction(id, amount, description, categoryId, expenseDate);
            return new OkObjectResult(updatedExpense);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new BadRequestObjectResult(e.Message);
        }
    }
}