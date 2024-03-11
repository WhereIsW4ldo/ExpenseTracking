using ExpenseTracking.Shared.DataModels;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracking.Shared.DAL;

public class ExpenseContext : DbContext
{
    public DbSet<Expense> Expenses { get; set; }
    public DbSet<Category> Categories { get; set; }
    
    public ExpenseContext(DbContextOptions options) : base(options) { }
}