using ExpenseTracking.Shared.DataModels;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracking.Shared.DAL;

public static class DbContextExtensions
{
    public static void Upsert<T>(this DbContext context, T entity) where T : class, GenericDataModel
    {
        context.Entry(entity).State = context.Set<T>().Find(entity.Id) != null
            ? EntityState.Modified 
            : EntityState.Added;
    }
    
    public static void UpsertRange<T>(this DbContext context, ICollection<T> entities) where T : class, GenericDataModel
    {
        foreach (var entity in entities)
        {
            context.Upsert(entity);
        }
    }
}
