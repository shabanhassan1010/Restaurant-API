using AutoMapper;
using Restaurant.Application.Restaurants.Commands.CreateResturant;
using Restaurant.Application.Restaurants.DTOS.Restaurant.Read;
using Restaurant.Application.Restaurants.DTOS.Restaurant.Update;
using Restaurant.Domain.Entities;

namespace Restaurant.Application.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region Restaurant
            CreateMap<Restaurantt, GetResturantDto>()
                .ForMember(d => d.Address, opt => opt.MapFrom(src => new Address
                {
                    Street = src.Address.Street,
                    City = src.Address.City,
                    PostalCode = src.Address.PostalCode,
                }));

            CreateMap<CreateResturantCommand, Restaurantt>()
                .ForMember(d=>d.Address , opt=>opt.MapFrom(src=> new Address
                {
                    Street = src.Address.Street,
                    City = src.Address.City,
                    PostalCode = src.Address.PostalCode,
                }));

            CreateMap<UpdateResturanctDto, Restaurantt>()
                .ForMember(d=>d.Address , opt=>opt.MapFrom(src=> new Address
                {
                    Street = src.Address.Street,
                    City = src.Address.City,
                    PostalCode = src.Address.PostalCode,
                }));
            #endregion
        }
    }
}
