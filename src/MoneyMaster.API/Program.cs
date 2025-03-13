using Microsoft.EntityFrameworkCore;
using MoneyMaster.Database;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connectionString = builder.Configuration.GetConnectionString("MoneyMasterContext");
builder.Services.AddDbContext<MoneyMasterContext>(options =>
    options.UseSqlServer(connectionString, option => option.EnableRetryOnFailure(10, TimeSpan.FromSeconds(30), null)));

builder.Services.AddControllers();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
