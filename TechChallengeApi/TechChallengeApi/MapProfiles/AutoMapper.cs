using AutoMapper;
using Domain.Entities;
using Resources.Dtos.Product;
using Resources.Dtos.ProductType;

namespace TechChallengeApi.MapProfiles
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<Product, ProductDto>()
                .ReverseMap();
            CreateMap<ProductPostDto, Product>()
                .ForMember(dest => dest.TypeId, opt => opt.Condition(src => src.TypeId != null))
                .ForMember(x => x.Type, opt => opt.Ignore())
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForMember(x => x.CreationDate, opt => opt.Ignore())
                .ForMember(x => x.CreationUser, opt => opt.Ignore())
                .ForMember(x => x.UpdateDate, opt => opt.Ignore())
                .ForMember(x => x.UpdateUser, opt => opt.Ignore())
                .ForMember(x => x.IsDeleted, opt => opt.Ignore())
                .ReverseMap();
            CreateMap<ProductPutDto, Product>()
                .ForMember(dest => dest.TypeId, opt => opt.Condition(src => src.TypeId != null))
                .ForMember(x => x.Type, opt => opt.Ignore())
                .ForMember(x => x.CreationDate, opt => opt.Ignore())
                .ForMember(x => x.CreationUser, opt => opt.Ignore())
                .ForMember(x => x.UpdateDate, opt => opt.Ignore())
                .ForMember(x => x.UpdateUser, opt => opt.Ignore())
                .ForMember(x => x.IsDeleted, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<ProductTypeDto, ProductType>()
                .ForMember(x => x.Products, opt => opt.Ignore())
                .ReverseMap();
            CreateMap<ProductTypePostDto, ProductType>()
                .ForMember(x => x.Products, opt => opt.Ignore())
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForMember(x => x.CreationDate, opt => opt.Ignore())
                .ForMember(x => x.CreationUser, opt => opt.Ignore())
                .ForMember(x => x.UpdateDate, opt => opt.Ignore())
                .ForMember(x => x.UpdateUser, opt => opt.Ignore())
                .ForMember(x => x.IsDeleted, opt => opt.Ignore())
                .ReverseMap();
            CreateMap<ProductTypePutDto, ProductType>()
                .ForMember(x => x.Products, opt => opt.Ignore())
                .ForMember(x => x.CreationDate, opt => opt.Ignore())
                .ForMember(x => x.CreationUser, opt => opt.Ignore())
                .ForMember(x => x.UpdateDate, opt => opt.Ignore())
                .ForMember(x => x.UpdateUser, opt => opt.Ignore())
                .ForMember(x => x.IsDeleted, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
