using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseTracking.Shared.DataModels;

public class Expense : GenericDataModel
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public int Id { get; set; }
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