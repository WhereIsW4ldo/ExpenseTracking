using Microsoft.EntityFrameworkCore;

namespace ExpenseTracking.Shared.DAL;

public class Seeder
{
    private readonly ExpenseContext _context;

    public Seeder(ExpenseContext context)
    {
        _context = context;
    }
    
    public void Seed()
    {
        _context.Database.Migrate();
    }
}