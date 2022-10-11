using Authentication.Contracts.Request.ApiRequest;
using AutoMapper;
using User.Contracts.Request.ApiRequest.Register;

namespace Authentication.Application.Mapper;

public class ApiMapper: Profile
{
    public ApiMapper()
    {
        CreateMap<RegisterRequest, CreateUser>();
    }
}