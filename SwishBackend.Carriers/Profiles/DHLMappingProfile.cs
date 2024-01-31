using AutoMapper;
using SwishBackend.Carriers.Models.NearestServicePointDHLModels;
using SwishBackend.Carriers.Models.NearestServicePointResponse;


namespace SwishBackend.Carriers.Profiles
{
    public class DHLMappingProfile : Profile
    {

        public DHLMappingProfile()
        {
            CreateMap<DHLServicePointLocation, DHLNearestServicePoint>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.location.ids.First().locationId))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.name))
            .ForMember(dest => dest.DeliveryAddress, opt => opt.MapFrom(src => new DeliveryAddressDTO
            {
                City = src.place.address.addressLocality,
                StreetName = src.place.address.streetAddress,
                PostalCode = src.place.address.postalCode,
                StreetNumber = null // DHL data might not contain street number
            }))
            .ForMember(dest => dest.OpeningHours, opt => opt.MapFrom(src => new OpeningHoursDTO
            {
                PostalServices = src.openingHours.Select(o => new PostalServiceDTO
                {
                    OpenDay = MapDayOfWeek(o.dayOfWeek),
                    CloseDay = MapDayOfWeek(o.dayOfWeek), // Adjust as needed
                    OpenTime = o.opens,
                    CloseTime = o.closes
                }).ToList()
            }))
            .ForMember(dest => dest.Geo, opt => opt.MapFrom(src => new DHLGeoDTO
            {
                Latitude = src.place.geo.latitude,
                Longitude = src.place.geo.longitude,
            }));
        }



        private string MapDayOfWeek(string schemaDayOfWeek)
        {
            // You can customize this mapping based on the schema.org day of week values
            // For example, you can map "http://schema.org/Monday" to "Monday"
            switch (schemaDayOfWeek)
            {
                case "http://schema.org/Monday":
                    return "Monday";
                case "http://schema.org/Tuesday":
                    return "Tuesday";
                case "http://schema.org/Wednesday":
                    return "Wednesday";
                case "http://schema.org/Thursday":
                    return "Thursday";
                case "http://schema.org/Friday":
                    return "Friday";
                case "http://schema.org/Saturday":
                    return "Saturday";
                case "http://schema.org/Sunday":
                    return "Sunday";
                default:
                    return schemaDayOfWeek;
            }
        }




    }
}
