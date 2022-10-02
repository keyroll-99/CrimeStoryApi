using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using User.Infrastructure;

namespace User.Api;

public static class UserModulesStartup
{
    public static IServiceCollection AddUserModules(this IServiceCollection services, IConfiguration configuration)
    {
        services.InstallUserInfrastructure(configuration);
        return services;
    }
    
}