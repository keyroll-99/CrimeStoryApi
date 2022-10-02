using Authentication.Api;
using Core;
using User.Api;

namespace Bootstrap;

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
        services.AddCoreProject();
        services.AddUserModules(_configuration);
        services.AddAuthenticationModules(_configuration);

        services.AddControllers();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (!env.IsDevelopment())
        {
            app.UseHttpsRedirection();
        }

        app.UseRouting();

        app.UseAuthentication();

        app.UseAuthenticationMiddleware();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapGet("/", context => context.Response.WriteAsync("Crime story API"));
        });
    }
}