using Authentication.Application.Services;
using Authentication.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace Authentication.Application;

public static class AuthenticationApplicationStartup
{
    public static IServiceCollection InstallAuthenticationApplication(this IServiceCollection services)
    {
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        return services;
    }
    
}
