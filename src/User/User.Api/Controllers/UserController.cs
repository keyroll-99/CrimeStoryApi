using Core.Attribute;
using Microsoft.AspNetCore.Mvc;
using User.Contracts;

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

    [HttpGet]
    [Authorize]
    public async Task<List<global::User.Contracts.Response.UserDto>> GetAllUsers()
    {
        return await _userService.GetAllUsersAsync();
    }
}