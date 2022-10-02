using Authentication.Api.Middleware;
using Authentication.Contracts.Models;
using Authentication.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Authentication.Api;

public static class AuthenticationModulesStartup
{
    public static IServiceCollection AddAuthenticationModules(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<JwtSettings>(configuration.GetSection("JWT"))
            .AddSingleton<JwtSettings>();
        
        services.InstallAuthenticationInfrastructure(configuration);
        return services;
    }

    public static IApplicationBuilder UseAuthenticationMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<AuthenticationMiddleware>();
        return app;
    }
}