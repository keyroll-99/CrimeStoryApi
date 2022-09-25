using System.Text;
using Authentication.Application.Services;
using Authentication.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Authentication.Application;

public static class AuthenticationApplicationStartup
{
    public static IServiceCollection InstallAuthenticationApplication(this IServiceCollection services, IConfiguration _configuration)
    {
        services.SetupAuthentication(_configuration);
        
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        return services;
    }

    private static IServiceCollection SetupAuthentication(this IServiceCollection services, IConfiguration _configuration)
    {
        return services;
    }
}
