using User.Contracts.Request.ApiRequest.Register;
using User.Contracts.Response;

namespace User.Contracts;

public interface IUserService
{
    Task<List<UserDto>> GetAllUsersAsync();
    Task<UserDto> GetUserAsync(long id);
    Task<UserDto> GetLoggedUserDate();
    Task<Response.UserDto> CreateUser(CreateUser form);
    Task<bool> VerifyPasswordAsync(string username, string password);
    Task<UserDto> GetUserAsync(string username);
}