using AutoMapper;
using MassTransitCommons.Common.Order;
using SwishBackend.MassTransitCommons.Common;
using SwishBackend.StoreItems.Models;
using SwishBackend.StoreItems.Models.Dtos.ProductEntitites;
using ProductCategory = SwishBackend.StoreItems.Models.ProductCategory;

namespace SwishBackend.StoreItems.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            //CreateMap<Product, ProductDto>().IncludeMembers(x => x.ProductCategory).ReverseMap();


            //create
            CreateMap<ProductDto, Product>()
                .ForMember(x => x.ProductCategory, o => o
                .MapFrom(s => s))
                .ReverseMap();

            CreateMap<ProductDto, ProductCreated>()
                .ReverseMap();

            CreateMap<ProductCategory, ProductDto>()
                .ReverseMap();



            CreateMap<Product, ProductResponseMessage>()
                .PreserveReferences()
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.ProductCategoryId))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.ProductCategory.CategoryName))
                .ReverseMap();

            CreateMap<ProductCategory, ProductCategoryResponseMessage>()
                .PreserveReferences()
                .ReverseMap();



            //update
            CreateMap<Product, ProductUpdated>()
                .IncludeMembers(x => x.ProductCategory)
                .ReverseMap();

            CreateMap<ProductCategory, ProductUpdated>();



        }
    }
}
