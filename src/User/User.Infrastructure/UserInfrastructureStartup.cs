using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using User.Application;
using User.Core;
using User.Core.Repositories;
using User.Infrastructure.Data;
using User.Infrastructure.Repository;

namespace User.Infrastructure;

public static class UserInfrastructureStartup
{
    public static IServiceCollection InstallUserInfrastructure (this IServiceCollection services, IConfiguration configuration)
    {
        services.InstallDb(configuration);
        services.InstallRepository();
        services.InstallUserApplication();        
        return services;
    }

    private static void InstallDb(this IServiceCollection services, IConfiguration configuration)
    {
        UserScriptRunner.RunScripts(configuration.GetConnectionString("DefaultConnection")!);
        services.AddDbContext<UserContext>(o => 
            o.UseNpgsql(configuration.GetConnectionString("DefaultConnection")!));
    }
    
    private static IServiceCollection InstallRepository(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        return services;
    }
}