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
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        services.AddCoreProject();
        services.AddUserModules();
        services.AddAuthenticationModules();

        services.AddControllers();

        services.AddCors(o =>
        {
            o.AddPolicy("localPolicy",
                p => { p.SetIsOriginAllowed(x => true).AllowAnyHeader().AllowAnyMethod().AllowCredentials(); });
        });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseCoreMiddleware();
        
        if (!env.IsDevelopment())
        {
            app.UseHttpsRedirection();
        }

        app.UseRouting();

        app.UseAuthentication();

        app.UseCors(x => x
            .SetIsOriginAllowed(o => true)
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());

        app.UseAuthenticationMiddleware();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapGet("/", context => context.Response.WriteAsync("Crime story API"));
        });
    }
}