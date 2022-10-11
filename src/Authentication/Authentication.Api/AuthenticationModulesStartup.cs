using Authentication.Api.Middleware;
using Authentication.Contracts.Models;
using Authentication.Infrastructure;
using Core.Utils;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Authentication.Api;

public static class AuthenticationModulesStartup
{
    public static IServiceCollection AddAuthenticationModules(this IServiceCollection services)
    {
        var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        var config = new ConfigurationBuilder()
            .SetBasePath(PathUtils.GetModulePath("Authentication.Api"))
            .AddJsonFile("appsettings.authentication.json")
            .AddJsonFile($"appsettings.authentication.{env}.json").Build();
        
        services.Configure<JwtSettings>(config.GetSection("JWT"))
            .AddSingleton<JwtSettings>();
        
        services.InstallAuthenticationInfrastructure(config);
        return services;
    }

    public static IApplicationBuilder UseAuthenticationMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<AuthenticationMiddleware>();
        return app;
    }
}