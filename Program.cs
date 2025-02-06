using flashcards_api;

using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = StaticSwaggerData.Version,
        Title = StaticSwaggerData.Title,
        Description = StaticSwaggerData.Description,
        Contact = StaticSwaggerData.Contact,
        License = StaticSwaggerData.License
    });
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

builder.Services.AddControllers();

// Add database context
builder.Services.AddDbContext<FlashcardsDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<FlashcardsDbContext>();
    try
    {
        context.Database.Migrate();
        Console.WriteLine("Database migration completed successfully");
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while migrating the database");
        Console.WriteLine($"Error during database migration: {ex.Message}");
    }
    
    // Add seed data if the table is empty
    if (!context.Flashcards.Any())
    {
        context.Flashcards.AddRange(
            new Flashcard 
            { 
                DeckId = 1, 
                Question = "What is Docker?", 
                Answer = "A platform for developing, shipping, and running applications in containers" 
            },
            new Flashcard 
            { 
                DeckId = 1, 
                Question = "What is .NET?", 
                Answer = "A free, open-source development platform for building many different types of applications" 
            },
            new Flashcard 
            { 
                DeckId = 1,
                Question = "What is PostgreSQL?", 
                Answer = "A powerful, open source object-relational database system" 
            }
        );
        context.SaveChanges();
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        // options.InjectStylesheet("/swagger-ui/custom.css");
    });
}

app.UseHttpsRedirection();
app.UseCors();
app.MapControllers();
app.Run();