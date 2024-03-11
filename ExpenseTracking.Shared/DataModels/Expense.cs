using System.ComponentModel.DataAnnotations;

namespace ExpenseTracking.Shared.DataModels;

public class Expense
{
    [Key]
    public int ID { get; set; }
    [Required]
    public double Amount { get; set; }
    [MaxLength(255)]
    public string Description { get; set; } = string.Empty;
    [Required]
    public Category Category { get; set; }
    [Required]
    public DateTime ExpenseDate { get; set; }
    [Required]
    public DateTime CreationDate { get; set; }
}