namespace WebApi.Helpers;

using AutoMapper;
using WebApi.Entities;
using WebApi.Models.Users;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        // User -> AuthenticateResponse
        CreateMap<UserRegister, AuthenticateResponse>();

        // RegisterRequest -> User
        CreateMap<RegisterRequest, UserRegister>();
    }
}