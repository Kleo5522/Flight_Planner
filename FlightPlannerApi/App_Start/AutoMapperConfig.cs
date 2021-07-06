using AutoMapper;
using FlightPlanner.Core.Models;
using FlightPlanner.Services.Models.Requests;
using FlightPlanner.Services.Models.Responses;

namespace FlightPlannerApi
{
    public class AutoMapperConfig
    {
        public static IMapper GetMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AddFlightRequest, Flight>()
                    .ForMember(dest => dest.Id,
                        opt => opt.Ignore());
                cfg.CreateMap<AddAirportRequest, Airport>()
                    .ForMember(dest => dest.AirportName,
                        opt => opt.MapFrom(src => src.Airport))
                    .ForMember(dest => dest.Id,
                        opt => opt.Ignore());
                cfg.CreateMap<Flight, FlightResponse>();
                cfg.CreateMap<Airport, AirportResponse>()
                    .ForMember(dest => dest.Airport,
                        opt => opt.MapFrom(src => src.AirportName));
            });
            config.AssertConfigurationIsValid();
            var mapper = config.CreateMapper();
            return mapper;
        }
    }
}