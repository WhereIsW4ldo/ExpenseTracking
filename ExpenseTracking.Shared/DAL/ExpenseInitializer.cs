//using ExpenseTracking.Shared.DataModels;

//namespace ExpenseTracking.Shared.DAL;

//public class ExpenseInitializer: System.Data.Entity.DropCreateDatabaseIfModelChanges<ExpenseContext>
//{
//    protected override void Seed(ExpenseContext context)
//    {
//        var foodCategory = new Category
//        {
//            Name = "Food"
//        };
        
//        var houseCategory = new Category
//        {
//            Name = "House"
//        };

//        var rentCategory = new Category
//        {
//            Name = "Rent"
//        };
        
//        houseCategory.SubCategories.Add(rentCategory);

//        var categories = new List<Category>
//        {
//            foodCategory, houseCategory, rentCategory
//        };
//        categories.ForEach(cat => context.Categories.Add(cat));
//        context.SaveChanges();

//        var exp0 = new Expense
//        {
//            ID = 1,
//            Amount = 10,
//            Category = houseCategory,
//            ExpenseDate = DateTime.Parse("2024-01-01"),
//            CreationDate = DateTime.Now
//        };
        
//        var exp1 = new Expense
//        {
//            ID = 2,
//            Amount = -5,
//            Description = "payback Asia donut",
//            Category = foodCategory,
//            ExpenseDate = DateTime.Parse("2024-01-10"),
//            CreationDate = DateTime.Now
//        };

//        var expenses = new List<Expense>
//        {
//            exp0, exp1
//        };
//        expenses.ForEach(ex => context.Expenses.Add(ex));
//        context.SaveChanges();
//    }
//}