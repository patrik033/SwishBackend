using AutoMapper;
using SwishBackend.Carriers.Models.NearestServicePointPostNordModels;
using SwishBackend.Carriers.Models.NearestServicePointResponse;


namespace SwishBackend.Carriers.Profiles
{
    public class PostNordMappingProfiles : Profile
    {
        public PostNordMappingProfiles()
        {
            CreateMap<ServicePoint, PostNordNearestServicePoint>()
           .ForMember(dest => dest.ServicePointId, opt => opt.MapFrom(src => src.ServicePointId))
           .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
           .ForMember(dest => dest.DeliveryAddress, opt => opt.MapFrom(src => src.DeliveryAddress))
           .ForMember(dest => dest.OpeningHours, opt => opt.MapFrom(src => src.OpeningHours))
           .ForMember(dest => dest.Coordinates,opt => opt.MapFrom(src => src.Coordinates));
            

            CreateMap<DeliveryAddress, DeliveryAddressDTO>();
            CreateMap<OpeningHours, OpeningHoursDTO>();
            CreateMap<PostalService, PostalServiceDTO>();
            CreateMap<Coordinate, CordinateDTO>();
        }
    }
}
