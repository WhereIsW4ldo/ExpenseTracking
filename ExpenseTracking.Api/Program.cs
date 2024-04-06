using ExpenseTracking.Api;
using ExpenseTracking.Domain.Services;
using ExpenseTracking.Shared.DAL;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Add database connections
var databaseConnection = builder.Configuration.GetConnectionString("DefaultConnection");

var userName = Environment.GetEnvironmentVariable("POSTGRES_USER");
var password = Environment.GetEnvironmentVariable("POSTGRES_PASSWORD");

var connectionString = string.Format(databaseConnection, userName, password);

builder.Services.AddDbContext<ExpenseContext>(options =>
{
    options.UseNpgsql(connectionString);
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<CategoryService>();
builder.Services.AddScoped<TransactionService>();
builder.Services.AddScoped<Seeder>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var seeder = scope.ServiceProvider.GetRequiredService<Seeder>();
    seeder.Seed();
}

app.UseAuthorization();

app.MapControllers();

app.Run();