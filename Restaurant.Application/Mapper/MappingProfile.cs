using AutoMapper;
using Restaurant.Application.DTOS.Restaurant.Read;
using Restaurant.Application.DTOS.Restaurant.Write;
using Restaurant.Domain.Entities;

namespace Restaurant.Application.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Restaurantt, GetResturantDto>()
                .ForMember(d => d.Address, opt => opt.MapFrom(src => new Address
                {
                    Street = src.Address.Street,
                    City = src.Address.City,
                    PostalCode = src.Address.PostalCode,
                }));

            CreateMap<CreateResturanctDto, Restaurantt>()
                .ForMember(d=>d.Address , opt=>opt.MapFrom(src=> new Address
                {
                    Street = src.Address.Street,
                    City = src.Address.City,
                    PostalCode = src.Address.PostalCode,
                }));
        }
    }
}
