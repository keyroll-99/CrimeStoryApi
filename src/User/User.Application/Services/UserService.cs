using AutoMapper;
using Core.Exceptions;
using User.Contracts;
using User.Contracts.Request.ApiRequest.Register;
using User.Core.Repositories;

namespace User.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserService(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<List<Contracts.Response.UserDto>> GetAllUsersAsync()
    {
        var users = await _userRepository.GetAll();

        return _mapper.Map<List<Contracts.Response.UserDto>>(users);
    }

    public async Task<Contracts.Response.UserDto> GetUserAsync(long id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        return _mapper.Map<Contracts.Response.UserDto>(user);
    }

    public async Task<Contracts.Response.UserDto> CreateUser(CreateUser form)
    {
        if (await _userRepository.UserExists(form.Username))
        {
            throw new ServerError();
        }
        
        var user = _mapper.Map<Core.Entities.User>(form);
        user = await _userRepository.AddAsync(user);

        return _mapper.Map<Contracts.Response.UserDto>(user);
    }
    
    public async Task<Contracts.Response.UserDto> GetUserAsync(string username)
    {
        var user = await _userRepository.GetByUsername(username);

        return _mapper.Map<Contracts.Response.UserDto>(user);
    }

    public async Task<bool> VerifyPasswordAsync(string username, string password)
    {
        var user = await _userRepository.GetByUsername(username);

        return BCrypt.Net.BCrypt.Verify(password, user.Password);
    }
}