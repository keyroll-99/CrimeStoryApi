using Authentication.Contracts;
using Authentication.Contracts.Request;
using Authentication.Contracts.Request.ApiRequest;
using Authentication.Contracts.Response;
using Authentication.Contracts.Response.ApiResponse;
using Core.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using User.Contracts;
using User.Contracts.Request.ApiRequest.Register;

namespace Authentication.Api.Controllers;

[Route("Api/[controller]")]
[ApiController]
[global::Core.Attribute.AllowAnonymous]
public class AuthenticationController: ControllerBase
{
    private const string RefreshTokenCookieName = "refreshToken";
    private readonly IAuthenticationService _authenticationService;
    private readonly IUserService _userService;

    public AuthenticationController(IAuthenticationService authenticationService, IUserService userService)
    {
        _authenticationService = authenticationService;
        _userService = userService;
    }

    [HttpPost("Login")]
    public async Task<AuthenticationResponse> Login(LoginRequest form)
    {
        if (!await _userService.VerifyPasswordAsync(form.Username, form.Password))
        {
            throw new ServerError("Invalid Password or Login", StatusCodes.Status400BadRequest);
        }

        var user = await _userService.GetUserAsync(form.Username);

        var refreshToken = await _authenticationService.GenerateRefreshToken(user.Id, GetIpAddress());
        SetTokenCookie(refreshToken);

        return new AuthenticationResponse
        {
            Jwt = _authenticationService.GenerateJwt(user.Id)
        };
    }

    [HttpPost("Register")]
    public async Task<AuthenticationResponse> Register(RegisterRequest form)
    {
        var user = await _userService.CreateUser(new CreateUser
        {
            Password = form.Password,
            Username = form.Username
        });
        
        var refreshToken = await _authenticationService.GenerateRefreshToken(user.Id, GetIpAddress());
        SetTokenCookie(refreshToken);
        var jwt = _authenticationService.GenerateJwt(user.Id);

        return new AuthenticationResponse
        {
            Jwt = jwt
        };
    }

    [HttpPost("Refresh")]
    public async Task<AuthenticationResponse> RefreshToken()
    {
        var refreshToken = GetRefreshTokenFromCookie();
        
        var ipAddress = GetIpAddress();

        var refreshResponse = await _authenticationService.RefreshToken(refreshToken, ipAddress);
        SetTokenCookie(refreshToken);

        return new AuthenticationResponse
        {
            Jwt = refreshResponse.Jwt
        };
    }

    private void SetTokenCookie(string token)
    {
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Expires = DateTimeOffset.Now.AddDays(7),
            Secure = true,
            SameSite = SameSiteMode.None
        };
        
        Response.Cookies.Append(RefreshTokenCookieName, token);
    }

    private string GetRefreshTokenFromCookie()
    {
        var token = Request.Cookies[RefreshTokenCookieName];
        if (token is null)
        {
            throw new ServerError("Missing refresh token", StatusCodes.Status401Unauthorized);
        }

        return token;
    }

    private string GetIpAddress() =>
        HttpContext.Connection.RemoteIpAddress?.MapToIPv4().ToString() ?? "";
}