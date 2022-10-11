using Authentication.Application;
using Authentication.Core;
using Authentication.Core.Repository;
using Authentication.Infrastructure.Data;
using Authentication.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Authentication.Infrastructure;

public static class AuthenticationInfrastructureStartup
{
    public static IServiceCollection InstallAuthenticationInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
    {
        
        services.AddDbContext<AuthenticationContext>(o => 
            o.UseNpgsql(configuration.GetConnectionString("DefaultConnection")!));

        AuthenticationScriptRunner.RunScripts(configuration.GetConnectionString("DefaultConnection")!);
        
        services.InstallRepository();
        services.InstallAuthenticationApplication();
        return services;
    }

    private static IServiceCollection InstallRepository(this IServiceCollection service)
    {
        service.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        return service;
    }
}