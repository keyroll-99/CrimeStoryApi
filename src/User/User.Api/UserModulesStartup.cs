using Core.Utils;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using User.Application.Mapper;
using User.Infrastructure;

namespace User.Api;

public static class UserModulesStartup
{
    public static IServiceCollection AddUserModules(this IServiceCollection services)
    {
        var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        var config = new ConfigurationBuilder()
            .SetBasePath(PathUtils.GetModulePath("User.Api"))
            .AddJsonFile("appsettings.user.json")
            .AddJsonFile($"appsettings.user.{env}.json").Build();
        
        services.InstallUserInfrastructure(config);
        services.AddAutoMapper(typeof(UserMapper));

        
        return services;
    }
}