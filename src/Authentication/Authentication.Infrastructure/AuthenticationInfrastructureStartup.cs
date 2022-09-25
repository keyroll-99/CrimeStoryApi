using Authentication.Application;
using Authentication.Core;
using Authentication.Core.Repository;
using Authentication.Infrastructure.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Authentication.Infrastructure;

public static class AuthenticationInfrastructureStartup
{
    public static IServiceCollection InstallAuthenticationInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.InstallRepository();
        services.InstallAuthenticationApplication(configuration);
        AuthenticationScriptRunner.RunScripts(configuration.GetConnectionString("DefaultConnection")!);
        return services;
    }

    private static IServiceCollection InstallRepository(this IServiceCollection service)
    {
        service.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        return service;
    }
}