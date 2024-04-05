using ExpenseTracking.Shared.DAL;
using ExpenseTracking.Shared.DataModels;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracking.Api;

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