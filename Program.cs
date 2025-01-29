// using flashcards_api;
//
// using Microsoft.EntityFrameworkCore;
// using Microsoft.OpenApi.Models;
//
// var builder = WebApplication.CreateBuilder(args);
//
// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen(options =>
// {
//     options.SwaggerDoc("v1", new OpenApiInfo
//     {
//         Version = "v1",
//         Title = "Flashcards API",
//         Description = "An ASP.NET Core Web API for managing Flashcards",
//         Contact = new OpenApiContact
//         {
//             Name = "bluka1",
//             Url = new Uri("https://github.com/bluka1")
//         }
//     });
// });
// builder.Services.AddControllers();
// builder.Services.AddDbContext<FlashcardsDbContext>(options =>
// {
//     options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
//     options.LogTo(Console.WriteLine, LogLevel.Information);
// });
//
// builder.Services.AddEndpointsApiExplorer();
//
// var app = builder.Build();
//
// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI(options => // UseSwaggerUI is called only in Development.
//     {
//         // options.InjectStylesheet("/swagger-ui/custom.css");
//     });
// }
//
// app.UseHttpsRedirection();
// app.MapControllers();
// app.Run();

// Program.cs

using flashcards_api;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder
            .AllowAnyOrigin()     // In production, you might want to restrict this to specific origins
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

// Add database context
builder.Services.AddDbContext<FlashcardsDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<FlashcardsDbContext>();
    
    // Apply migrations
    db.Database.Migrate();
    
    // Add seed data if the table is empty
    if (!db.Flashcards.Any())
    {
        db.Flashcards.AddRange(
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
        db.SaveChanges();
    }
}

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

// Define endpoints
app.MapGet("/flashcards", async (FlashcardsDbContext db) =>
        await db.Flashcards.ToListAsync())
    .WithName("GetFlashcards")
    .WithOpenApi();

app.Run();