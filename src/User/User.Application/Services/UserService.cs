using AutoMapper;
using Core.Exceptions;
using Core.Models;
using Microsoft.Extensions.Options;
using User.Contracts;
using User.Contracts.Request.ApiRequest.Register;
using User.Contracts.Response;
using User.Core.Repositories;

namespace User.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly LoggedUser _loggedUser;

    public UserService(IUserRepository userRepository, IMapper mapper, IOptions<LoggedUser> loggedUser)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _loggedUser = loggedUser.Value;
    }

    public async Task<List<UserDto>> GetAllUsersAsync()
    {
        var users = await _userRepository.GetAll();

        return _mapper.Map<List<UserDto>>(users);
    }

    public async Task<UserDto> GetUserAsync(long id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        return _mapper.Map<UserDto>(user);
    }

    public async Task<UserDto> GetLoggedUserDate()
    {
        var user = await _userRepository.GetByIdAsync(_loggedUser.LoggedUserData.Id);
        return _mapper.Map<UserDto>(user);
    }

    public async Task<UserDto> CreateUser(CreateUser form)
    {
        if (await _userRepository.UserExists(form.Username))
        {
            throw new ServerError();
        }

        var user = _mapper.Map<Core.Entities.User>(form);
        user = await _userRepository.AddAsync(user);

        return _mapper.Map<UserDto>(user);
    }

    public async Task<UserDto> GetUserAsync(string username)
    {
        var user = await _userRepository.GetByUsername(username);

        return _mapper.Map<UserDto>(user);
    }

    public async Task<bool> VerifyPasswordAsync(string username, string password)
    {
        var user = await _userRepository.GetByUsername(username);

        return BCrypt.Net.BCrypt.Verify(password, user.Password);
    }
}