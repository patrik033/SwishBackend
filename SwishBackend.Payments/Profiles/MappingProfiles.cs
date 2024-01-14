using AutoMapper;
using Stripe.Checkout;
using SwishBackend.MassTransitCommons.Common.Payment;
using SwishBackend.MassTransitCommons.Models;


namespace SwishBackend.Payments.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {

            //CreateMap<Session, CreatePaymentSessionResponse>()
                
            //    .ForMember(d => d.Session.AmountTotal, opt => opt.MapFrom(src => src.AmountTotal))
            //    .ForMember(d => d.Session.AmountSubtotal, opt => opt.MapFrom(src => src.AmountSubtotal))
            //    .ForMember(d => d.Session.ClientSecret, opt => opt.MapFrom(src => src.ClientSecret))
            //    .ForMember(d => d.Session.CustomerEmail, opt => opt.MapFrom(src => src.CustomerEmail)).ReverseMap();
                          
     




        }
    }
}
