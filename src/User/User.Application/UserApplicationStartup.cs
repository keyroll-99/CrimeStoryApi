using Microsoft.Extensions.DependencyInjection;
using User.Application.Mapper;
using User.Application.Services;
using User.Contracts;

namespace User.Application;

public static class UserApplicationStartup
{
    public static IServiceCollection InstallUserApplication (this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        
        return services;
    }
}