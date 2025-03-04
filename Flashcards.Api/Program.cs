using Flashcards.Api;
using Flashcards.Application;
using Flashcards.Infrastructure;
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
    options.AddDefaultPolicy(buildr =>
    {
        buildr
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

builder.Services.AddControllers();

builder.Services
    .AddInfrastructureServices(builder.Configuration)
    .AddApplicationDeps();

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// Run migrations
app.MigrateDatabase();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    // TODO: options.InjectStylesheet("/swagger-ui/custom.css");
});

app.UseHttpsRedirection();
app.UseCors();
app.MapControllers();
app.Run();

public partial class Program { }