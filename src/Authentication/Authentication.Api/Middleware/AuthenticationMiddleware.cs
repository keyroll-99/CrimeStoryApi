using Authentication.Contracts;
using Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using User.Contracts;

namespace Authentication.Api.Middleware;

public class AuthenticationMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IOptions<LoggedUser> _loggedUser;
    private const string AuthorizationHeader = "Authorization";

    public AuthenticationMiddleware(RequestDelegate next, IOptions<LoggedUser> loggedUser)
    {
        _next = next;
        _loggedUser = loggedUser;
    }

    public async Task Invoke(HttpContext context, IAuthenticationService authenticationService,
        IUserService userService)
    {
        var token = context.Request.Headers[AuthorizationHeader].ToString().Split(" ").Last();

        var userId = authenticationService.ValidateJwt(token);

        if (userId.HasValue)
        {
            var user = await userService.GetUserAsync(userId.Value);
            _loggedUser.Value.SetLoggedUser(new LoggedUserData { Id = user.Id, Username = user.Username });
            context.Items["IsLogged"] = "true";
        }

        await _next(context);
    }
}