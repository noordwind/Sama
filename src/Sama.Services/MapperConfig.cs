using AutoMapper;
using Sama.Core.Domain.Identity;
using Sama.Core.Domain.Ngos;
using Sama.Core.Domain.Shared;
using Sama.Services.Identity.Dtos;
using Sama.Services.Ngos.Dtos;
using Sama.Services.Shared.Dtos;

namespace Sama.Services
{
    public static class MapperConfig
    {
        public static IMapper Get()
            => new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Ngos.Commands.Models.Child, ChildInfoDto>();
                cfg.CreateMap<Shared.Commands.Models.Location, LocationDto>();
                cfg.CreateMap<ChildInfo, ChildInfoDto>();
                cfg.CreateMap<Donation, DonationDto>();
                cfg.CreateMap<Location, LocationDto>();
                cfg.CreateMap<Ngo, NgoDto>();
                cfg.CreateMap<Payment, PaymentDto>();
                cfg.CreateMap<User, UserDto>();
            }).CreateMapper();
    }
}