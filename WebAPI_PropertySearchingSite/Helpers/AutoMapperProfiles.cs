using AutoMapper;
using WebAPI_PropertySearchingSite.Dtos;
using WebAPI_PropertySearchingSite.Models;

namespace WebAPI_PropertySearchingSite.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<City, CityDto>().ReverseMap();
            
            CreateMap<PropertyType, KeyValuePairDto>().ReverseMap();

            CreateMap<FurnishingType, KeyValuePairDto>().ReverseMap();

            CreateMap<Property, PropertyDto>().ReverseMap();

            CreateMap<Photo, PhotoDto>().ReverseMap();

            CreateMap<Property, PropertyListDto>()
                .ForMember(d => d.City, opt => opt.MapFrom(src => src.City.Name))
                .ForMember(d => d.Country, opt => opt.MapFrom(src => src.City.Country))
                .ForMember(d => d.PropertyType, opt => opt.MapFrom(src => src.PropertyType.Name))
                 .ForMember(d => d.Photo, opt => opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsPrimary).ImageUrl))
                .ForMember(d => d.FurnishingType, opt => opt.MapFrom(src => src.FurnishingType.Name));

            CreateMap<Property, PropertyDetailDto>()
               .ForMember(d => d.City, opt => opt.MapFrom(src => src.City.Name))
               .ForMember(d => d.Country, opt => opt.MapFrom(src => src.City.Country))
               .ForMember(d => d.PropertyType, opt => opt.MapFrom(src => src.PropertyType.Name))
               .ForMember(d => d.FurnishingType, opt => opt.MapFrom(src => src.FurnishingType.Name));
        }
    }
}
