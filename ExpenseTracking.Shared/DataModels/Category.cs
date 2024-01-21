namespace ExpenseTracking.Shared.DataModels;

public class Category
{
    public string Name { get; set; }
    public virtual List<Category> SubCategories { get; set; } = new List<Category>();
}