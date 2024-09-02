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
    
    [HttpPost("{name}")]
    public IActionResult AddCategory(string name)
    {
        try
        {
            var category = _service.AddCategory(name);
            return new OkObjectResult(category);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new NotFoundResult();
        }
    }
    
    [HttpDelete("{id:int}")]
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
    
    [HttpPut]
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