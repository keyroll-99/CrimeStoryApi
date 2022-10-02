using Core.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Core;

public static class CoreStartup
{
    public static IServiceCollection AddCoreProject(this IServiceCollection services)
    {
        services.AddScoped<LoggedUser>();

        return services;
    }
}