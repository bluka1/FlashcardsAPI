using Flashcards.Application.Common.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;

namespace Flashcards.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<FlashcardsDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
        services.AddScoped<IFlashcardsRepository, FlashcardsRepository>();
        return services;
    }

    public static void MigrateDatabase(this WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<FlashcardsDbContext>();
            try
            {
                context.Database.EnsureCreated();
                context.Database.Migrate();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
                // var logger = services.GetRequiredService<ILogger<Program>>();
                // logger.LogError(ex, "An error occurred while migrating the database");
            }
        }
    }
}