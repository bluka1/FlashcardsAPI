using Microsoft.Extensions.DependencyInjection;

namespace Flashcards.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationDeps(this IServiceCollection services)
    {
        services.AddMediatR(options =>
        {
            options.RegisterServicesFromAssemblyContaining(typeof(DependencyInjection));
        });
        return services;
    }
}