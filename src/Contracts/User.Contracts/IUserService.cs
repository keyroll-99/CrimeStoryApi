using User.Contracts.Reponse;

namespace User.Contracts;

public interface IUserService
{
    Task<List<UserResponse>> GetAllUsersAsync();
    Task<UserResponse> GetUserAsync(long id);
}