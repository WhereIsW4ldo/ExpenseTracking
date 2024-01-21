namespace ExpenseTracking.Shared.DataModels;

public class Expense
{
    public int ID { get; set; }
    public double Amount { get; set; }
    public string Description { get; set; } = string.Empty;
    
    public string CategoryName { get; set; }
    
    public DateTime ExpenseDate { get; set; }
    public DateTime CreationDate { get; set; }
}