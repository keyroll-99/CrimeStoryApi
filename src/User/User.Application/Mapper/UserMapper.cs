using AutoMapper;
using User.Contracts.Request.ApiRequest.Register;

namespace User.Application.Mapper;

public class UserMapper : Profile
{
    public UserMapper()
    {
        CreateMap<CreateUser, Core.Entities.User>()
            .ForMember(
                x => x.Hash,
                opt => opt.MapFrom((o => Guid.NewGuid())))
            .ForMember(
                x => x.Password,
                opt => opt.MapFrom(o => BCrypt.Net.BCrypt.HashPassword(o.Password))
            );

        CreateMap<Core.Entities.User, Contracts.Response.UserDto>();
    }
}