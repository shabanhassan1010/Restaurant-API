using AutoMapper;
using Restaurant.Application.Dishes.Commands.CreateDish;
using Restaurant.Application.Dishes.DTOS.Dish;
using Restaurant.Domain.Entities;

namespace Restaurant.Application.Mapper
{
    public class DishProfile : Profile
    {
        public DishProfile()
        {
            CreateMap<Dish, GetDishDto>()
                    .ForMember(dest => dest.RestaurantName, opt => opt.MapFrom(src => src.Restaurant.Name));

            CreateMap<CreateDishCommand, Dish>();
        }
    }
}
