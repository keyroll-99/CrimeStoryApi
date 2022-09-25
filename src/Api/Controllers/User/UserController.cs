using Microsoft.AspNetCore.Mvc;
using User.Contracts;
using User.Contracts.Reponse;

namespace Api.Controllers.User;

[Route("Api/[controller]")]
[ApiController]
public class UserController
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<List<UserResponse>> GetAllUsers()
    {
        return await _userService.GetAllUsersAsync();
    }
}