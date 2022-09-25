using Api.Middleware;
using Authentication.Contracts.Models;
using Authentication.Infrastructure;
using Core.Models;
using User.Infrastructure;

namespace Api;

public class Startup
{
    private readonly IConfiguration _configuration;
    private readonly IHostEnvironment _environment;

    public Startup(IConfiguration configuration, IHostEnvironment environment)
    {
        _configuration = configuration;
        _environment = environment;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.Configure<LoggedUser>(_configuration.GetSection("LoggedUser"))
            .AddScoped<LoggedUser>();

        services.Configure<JwtSettings>(_configuration.GetSection("JWT"))
            .AddScoped<JwtSettings>();
        
        services.InstallUserInfrastructure(_configuration);
        services.InstallAuthenticationInfrastructure(_configuration);

        services.AddControllers();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (!env.IsDevelopment())
        {
            app.UseHttpsRedirection();
        }
        app.UseRouting();

        app.UseMiddleware<AuthenticationMiddleware>();
        
        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
}