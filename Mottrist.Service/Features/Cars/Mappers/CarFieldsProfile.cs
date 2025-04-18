﻿using AutoMapper;
using Feature.Car.DTOs;
using Mottrist.Domain.Entities.CarDetails;
using Mottrist.Service.Features.Cars.DTOs.CarFieldsDTOs;

namespace Mottrist.Service.Features.Cars.Mappers
{
    class CarFieldsProfile : Profile
    {
        public CarFieldsProfile()
        {
            CreateMap<Model, CarModelDto>().ReverseMap();
            CreateMap<BodyType, CarBodyTypeDto>().ReverseMap();
            CreateMap<Brand, CarBrandDto>().ReverseMap();
            CreateMap<FuelType, CarFuelTypeDto>().ReverseMap();
            CreateMap<Color, CarColorDto>().ReverseMap();
        }
    }
}
