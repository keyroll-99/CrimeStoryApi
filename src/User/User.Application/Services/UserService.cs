using User.Contracts;
using User.Contracts.Reponse;
using User.Core.Repositories;

namespace User.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<List<UserResponse>> GetAllUsersAsync()
    {
        var users = await _userRepository.GetAll();

        return users.Select(x => new UserResponse { Username = x.Username, Hash = x.Hash, Id = x.Id}).ToList();
    }

    public Task<UserResponse> GetUserAsync(long id)
    {
        throw new NotImplementedException();
    }
}