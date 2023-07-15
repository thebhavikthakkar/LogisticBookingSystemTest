using AutoMapper;
using LogisticBookingSystem.Models.Requests;
using LogisticBookingSystem.Models.Responses;
using LogisticsBookingSystem.Core.Entities;

namespace LogisticBookingSystem
{
    public class MappingProfiles
    {
        public static void InstallServices(IServiceCollection services)
        {
            MapperConfiguration mapperConfig = new(config =>
            {
                _ = config.CreateMap<Location, LocationModel>().ReverseMap();
                config.CreateMap<Booking, BookingModel>()
             .ForMember(dest => dest.Time, opt => opt.MapFrom(src => src.Time))
             .ReverseMap()
             .ForMember(dest => dest.Time, opt => opt.MapFrom(src => src.Time));

                _ = config.CreateMap<Booking, BookingDto>().ReverseMap();
                _ = config.CreateMap<Location, LocationDto>().ReverseMap();


            });

            IMapper mapper = mapperConfig.CreateMapper();
            _ = services.AddSingleton(mapper);
        }

    }
}
