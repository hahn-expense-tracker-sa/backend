using Microsoft.EntityFrameworkCore;
using ExpenseTrackerBackend.models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularDev",
        policy => policy.WithOrigins("http://localhost:4200")  
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});


// Add DbContext for In-Memory Database
builder.Services.AddDbContext<ExpenseTrackerContext>(options =>
    options.UseInMemoryDatabase("ExpenseTrackerDb"));

var app = builder.Build();

// Add default categories if not already present
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ExpenseTrackerContext>();
    if (!db.Categories.Any())  // Check if categories already exist
    {
        db.Categories.AddRange(
            new Category { Name = "Food" },
            new Category { Name = "Transport" },
            new Category { Name = "Entertainment" },
            new Category { Name = "Utilities" }
        );
        db.SaveChanges();
    }
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Enable CORS
app.UseCors("AllowAngularDev");


app.UseRouting();  
app.MapControllers();
app.Run();
