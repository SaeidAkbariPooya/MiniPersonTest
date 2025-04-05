using AutoMapper;
using MiniPerson.Application.DTO;
using MiniPerson.Domain.Entities;

namespace MiniPerson.Application.Profiles
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Person, PersonDto>().ReverseMap();
            ////CreateMap<Source, Destination>().ReverseMap();

            //CreateMap<Product, ProductDto>()
            //.ForMember(dest => dest.PriceWithComma, opt => opt.MapFrom(src => String.Format("{0:n0}", src.Price)))
            //.ReverseMap();

            //CreateMap<Product, GetProductQueryResponse>()
            //   .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.ProductName.ToUpper()))
            //   .ForMember(dest => dest.PriceWithComma, opt => opt.MapFrom(src => String.Format("{0:n0}", src.Price)));

            //CreateMap<AddRefreshTokenNotification, UserRefreshToken>()
            //.ForMember(dest => dest.CreateDate, opt => opt.MapFrom(src => DateTime.Now))
            //.ForMember(dest => dest.IsValid, opt => opt.MapFrom(src => true));
        }
    }
}
