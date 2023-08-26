using AutoMapper;
using TransportExpress.Domain.Commands.User;
using TransportExpress.Domain.Entities;

namespace TransportExpress.AutoMapper
{
    public class ConfigurationProfile : Profile
    {
        public ConfigurationProfile()
        {
            CreateMap<User, AuthenticateUserCommand>().ReverseMap();
            CreateMap<User, CreateUserCommand>().ReverseMap();
        }
    }
}