using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using PropertyAPI.DtoModels;
using PropertyAPI.DtoModels.BuildingProperty;
using PropertyAPI.DtoModels.City;
using PropertyAPI.Models;

namespace PropertyAPI.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<City, CityDto>().ReverseMap();

            CreateMap<City, CityUpdateDto>().ReverseMap();

            CreateMap<BuildingProperty, BuildingPropertyDto>()
                .ForMember(m => m.City, opt => opt.MapFrom(src => src.City.Name))
                .ForMember(m => m.Country, opt => opt.MapFrom(src => src.City.Country))
                .ForMember(m => m.PropertyType, opt => opt.MapFrom(src => src.PropertyType.Name))
                .ForMember(m => m.FurnitureType, opt => opt.MapFrom(src => src.FurnitureType.Name))
                .ForMember(m => m.PostedBy, opt => opt.MapFrom(src => src.User.Username));

            CreateMap<PropertyType, IdNamePairDto>().ReverseMap();
            CreateMap<FurnitureType, IdNamePairDto>().ReverseMap();
        }
    }
}