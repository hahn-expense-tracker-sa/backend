using Microsoft.EntityFrameworkCore;
using ExpenseTrackerBackend.models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add DbContext for In-Memory Database
builder.Services.AddDbContext<ExpenseTrackerContext>(options =>
    options.UseInMemoryDatabase("ExpenseTrackerDb"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.Run();
