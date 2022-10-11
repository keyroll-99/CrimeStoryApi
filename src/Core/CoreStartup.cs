using Core.Middleware;
using Core.Models;
using Microsoft.AspNetCore.Builder;
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

    public static IApplicationBuilder UseCoreMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<CatchErrorMiddleware>();
        return app;
    }
}