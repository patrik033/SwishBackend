using AutoMapper;
using SwishBackend.MassTransitCommons.Models;
using SwishBackend.Orders.Models;

namespace SwishBackend.Orders.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {

            CreateMap<ShoppingCartOrder, ShoppingCartOrderMessage>()
                .PreserveReferences()
                .ReverseMap();

            CreateMap<ShoppingCartItem, ShoppingCartItemMessage>()
                .PreserveReferences()
                .ReverseMap();

            


        }
    }
}
