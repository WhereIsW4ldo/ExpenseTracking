using ExpenseTracking.Shared.DataModels;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracking.Shared.DAL;

public class ExpenseContext : DbContext
{
    public DbSet<Expense> Expenses { get; set; }
    public DbSet<Category> Categories { get; set; }

    public ExpenseContext(DbContextOptions options) : base(options)
    {
        this.ChangeTracker.LazyLoadingEnabled = false;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var categories = new List<Category>
         {
             new() { Id = 1, Name = "Groceries" },
             new() { Id = 2, Name = "Utilities" },
             new() { Id = 3, Name = "Rent" },
             new() { Id = 4, Name = "Transportation" },
             new() { Id = 5, Name = "Entertainment" },
             new() { Id = 6, Name = "Health" },
             new() { Id = 7, Name = "Insurance" },
             new() { Id = 8, Name = "Other" }
         };

        modelBuilder.Entity<Category>()
            .HasData(categories);
    }
}