using User.Contracts.Request.ApiRequest.Register;

namespace User.Contracts;

public interface IUserService
{
    Task<List<Response.UserDto>> GetAllUsersAsync();
    Task<Response.UserDto> GetUserAsync(long id);
    Task<Response.UserDto> CreateUser(CreateUser form);
    Task<bool> VerifyPasswordAsync(string username, string password);
    Task<Response.UserDto> GetUserAsync(string username);
}