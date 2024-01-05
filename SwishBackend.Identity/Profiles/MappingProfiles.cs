using AutoMapper;
using MassTransitCommons.Common.Email;
using SwishBackend.Identity.Models;
using SwishBackend.Models.Dto;

namespace SwishBackend.Identity.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<ApplicationUser, EmailConfirmationMessage>().ReverseMap();
         
        }
    }
}
