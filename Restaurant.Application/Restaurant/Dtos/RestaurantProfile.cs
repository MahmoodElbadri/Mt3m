using AutoMapper;
using Restaurant.Domain.Entities;

namespace Restaurant.Application.Restaurant.Dtos;

public class RestaurantProfile : Profile
{
    public RestaurantProfile()
    {
        CreateMap<Domain.Entities.Restaurant, RestaurantDto>()
            .ForMember(tmp => tmp.City, opt => opt.MapFrom(src => src.Address == null ? null : src.Address.City))
            .ForMember(tmp => tmp.Street, opt => opt.MapFrom(src => src.Address == null ? null : src.Address.Street))
            .ForMember(tmp => tmp.PostalCode, opt => opt.MapFrom(src => src.Address == null ? null : src.Address.PostalCode))
            .ForMember(tmp => tmp.Dishes, opt => opt.MapFrom(src => src.Dishes));


    }
}
