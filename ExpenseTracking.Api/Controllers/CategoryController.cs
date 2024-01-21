using ExpenseTracking.Domain.Services;
using ExpenseTracking.Shared.DataModels;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracking.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class CategoryController
{
    private readonly CategoryService _service;

    public CategoryController(CategoryService service)
    {
        _service = service;
    }

    [HttpGet(Name = "GetCategories")]
    public IEnumerable<Category> Get()
    {
        try
        {
            return _service.GetCategories();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new List<Category>();
        }
    }
}