using Core.Attribute;
using Microsoft.AspNetCore.Mvc;
using User.Contracts;
using User.Contracts.Response;

namespace User.Api.Controllers;

[Route("Api/[controller]")]
[ApiController]
[Authorize]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    
    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("Logged-user")]
    public async Task<UserDto> GetLoggedUserData()
    {
        return await _userService.GetLoggedUserDate();
    }

    [HttpGet]
    public async Task<List<Contracts.Response.UserDto>> GetAllUsers()
    {
        return await _userService.GetAllUsersAsync();
    }
}