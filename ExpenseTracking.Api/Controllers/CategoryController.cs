using ExpenseTracking.Domain.Services;
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

    [HttpGet]
    public IActionResult GetCategories()
    {
        try
        {
            var categories = _service.GetCategories();
            return new OkObjectResult(categories);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new NotFoundResult();
        }
    }
    
    [HttpPut]
    public IActionResult AddCategory(string name)
    {
        try
        {
            _service.AddCategory(name);
            return new OkResult();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new NotFoundResult();
        }
    }
    
    [HttpDelete]
    public IActionResult RemoveCategory(int id)
    {
        try
        {
            var removedCategory = _service.RemoveCategory(id);
            return new OkObjectResult(removedCategory);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new NotFoundResult();
        }
    }
    
    [HttpPost]
    public IActionResult UpdateCategory(int id, string name)
    {
        try
        {
            var updatedCategory = _service.UpdateCategory(id, name);
            return new OkObjectResult(updatedCategory);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new NotFoundResult();
        }
    }
}