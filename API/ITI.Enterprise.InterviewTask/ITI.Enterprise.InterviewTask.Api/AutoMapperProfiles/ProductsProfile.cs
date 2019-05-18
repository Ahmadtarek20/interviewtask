using AutoMapper;
using ITI.Enterprise.InterviewTask.DomainClasses;

namespace ITI.Enterprise.InterviewTask.Api.AutoMapperProfiles
{
    public class ProductsProfile : Profile
    {

        public ProductsProfile()
        {

            CreateMap<Product, DTO.ProductDto>()
                .ForMember(dest => dest.CompanyName,
                    ops => ops.MapFrom(src => src.Company.Name));

            CreateMap<DTO.ProductDto, Product>()
                .ForPath(dest=>dest.Company.Name,
                    ops=> ops.MapFrom(src=>src.CompanyName));

              //CreateMap<ProductModificationDto, Product>().ForMember(dest =>
              //{
              //    dest.
              //})
            //CreateMap<ProductCreationDto, Product>().ForPath(dest=> dest.ImageUrl,
            //    options=>options.MapFrom(src=>Path.Combine(src.Photo.Name));
        }
    }
}
