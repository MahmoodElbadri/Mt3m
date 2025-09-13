using AutoMapper;

namespace Restaurant.Application.Dishes.Dtos;

public class DishesProfile:Profile
{
    public DishesProfile()
    {
        CreateMap<Domain.Entities.Dish, DishDto>()
            .ReverseMap();
    }
}
