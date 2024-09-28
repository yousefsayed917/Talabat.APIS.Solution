using AutoMapper;
using Talabat.APIS.DTOS;
using Talabat.APIS.Helpers;
using Talabat.Core.Eintites;

namespace Talabat.APIS.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles() 
        {
            CreateMap<Product, ProductToReturnDto>()
                .ForMember(d => d.ProductType, d => d.MapFrom(d => d.ProductType.Name))
                .ForMember(s => s.ProductBrand, s => s.MapFrom(s => s.ProductBrand.Name))
                .ForMember(p => p.PictureUrl, p => p.MapFrom<ProductPictureUrlResolver>());

        }
    }
}
