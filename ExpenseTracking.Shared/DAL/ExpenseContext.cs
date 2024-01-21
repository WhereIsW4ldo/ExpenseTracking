using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using ExpenseTracking.Shared.DataModels;

namespace ExpenseTracking.Shared.DAL;

public class ExpenseContext : DbContext
{
    public ExpenseContext() : base("ExpenseContext") { }
    
    public DbSet<Expense> Expenses { get; set; }
    public DbSet<Category> Categories { get; set; }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
        modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
    }
}