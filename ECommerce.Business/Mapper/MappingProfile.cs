// LightningBits
using System;
using AutoMapper;
using BlazorWeb.Server;
using ECommerce_Models;

namespace ECommerce_Business.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, CategoryDTO>().ReverseMap();
                //CreateMap<CategoryDTO, Category>()
        }
    }
       
}

