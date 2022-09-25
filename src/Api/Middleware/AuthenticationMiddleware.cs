using Authentication.Contracts;
using Core.Models;
using Microsoft.Extensions.Options;
using User.Contracts;

namespace Api.Middleware;

public class AuthenticationMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IOptions<LoggedUser> _loggedUser;
    private readonly string _authorizationHeader = "Authorization";

    public AuthenticationMiddleware(RequestDelegate next, IOptions<LoggedUser> loggedUser)
    {
        _next = next;
        _loggedUser = loggedUser;
    }

    public async Task Invoke(HttpContext context, IAuthenticationService authenticationService,
        IUserService userService)
    {
        var token = context.Request.Headers[_authorizationHeader].ToString();
        var userId = authenticationService.ValidateJwtToken(token);

        if (userId.HasValue)
        {
            var user = await userService.GetUserAsync(userId.Value);
            _loggedUser.Value.Id = user.Id;
            _loggedUser.Value.Username = user.Username;
            context.Items["IsLogged"] = "true";
        }

        await _next(context);
    }
}