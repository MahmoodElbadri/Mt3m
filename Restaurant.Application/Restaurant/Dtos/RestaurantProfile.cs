using AutoMapper;
using Restaurant.Domain.Entities;

namespace Restaurant.Application.Restaurant.Dtos;

public class RestaurantProfile : Profile
{
    public RestaurantProfile()
    {
        //we can't put reverse mapping map because of Address object type in Restaurant entity
        CreateMap<RestaurantDto, Domain.Entities.Restaurant>()
            .ForMember(tmp => tmp.Address, opt => opt.MapFrom(src => new Address
            {
                City = src.City,
                Street = src.Street,
                PostalCode = src.PostalCode
            }))
            .ForMember(tmp => tmp.Dishes, opt => opt.MapFrom(src => src.Dishes));

        CreateMap<RestaurantCreateDto, Domain.Entities.Restaurant>()
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => new Address
            {
                City = src.City,
                Street = src.Street,
                PostalCode = src.PostalCode
            }));

        CreateMap<Domain.Entities.Restaurant, RestaurantDto>()
            .ForMember(tmp => tmp.City, opt => opt.MapFrom(src => src.Address == null ? null : src.Address.City))
            .ForMember(tmp => tmp.Street, opt => opt.MapFrom(src => src.Address == null ? null : src.Address.Street))
            .ForMember(tmp => tmp.PostalCode, opt => opt.MapFrom(src => src.Address == null ? null : src.Address.PostalCode))
            .ForMember(tmp => tmp.Dishes, opt => opt.MapFrom(src => src.Dishes));


    }
}
