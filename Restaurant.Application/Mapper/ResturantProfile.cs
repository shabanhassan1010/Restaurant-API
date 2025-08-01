﻿using AutoMapper;
using Restaurant.Application.Dishes.DTOS.Dish;
using Restaurant.Application.Restaurants.Commands.CreateResturant;
using Restaurant.Application.Restaurants.Commands.UpdateResturant;
using Restaurant.Application.Restaurants.DTOS.Restaurant.Read;
using Restaurant.Domain.Entities;

namespace Restaurant.Application.Mapper
{
    public class ResturantProfile : Profile
    {
        public ResturantProfile()
        {
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

            CreateMap<UpdateResturantCommand, Restaurantt>()
                .ForMember(d=>d.Address , opt=>opt.MapFrom(src=> new Address
                {
                    Street = src.Address.Street,
                    City = src.Address.City,
                    PostalCode = src.Address.PostalCode,
                }));

        }
    }
}
