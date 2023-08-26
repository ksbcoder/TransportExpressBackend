using AutoMapper;
using TransportExpress.Domain.Commands.Logistic;
using TransportExpress.Domain.Commands.Product;
using TransportExpress.Domain.Commands.Storage;
using TransportExpress.Domain.Commands.Transport;
using TransportExpress.Domain.Commands.User;
using TransportExpress.Domain.Entities;

namespace TransportExpress.AutoMapper
{
    public class ConfigurationProfile : Profile
    {
        public ConfigurationProfile()
        {
            #region User
            CreateMap<User, AuthenticateUserCommand>().ReverseMap();
            CreateMap<User, CreateUserCommand>().ReverseMap();
            #endregion

            #region Transport
            CreateMap<Transport, CreateTransportCommand>().ReverseMap();
            #endregion

            #region Product
            CreateMap<Product, CreateProductCommand>().ReverseMap();
            #endregion

            #region Storage
            CreateMap<Storage, CreateStorageCommand>().ReverseMap();
            #endregion

            #region Logistic
            CreateMap<Logistic, CreateLogisticCommand>().ReverseMap();
            CreateMap<Logistic, UpdateLogisticCommand>().ReverseMap();
            #endregion
        }
    }
}