// LightningBits
using System;
using AutoMapper;
using BlazorWeb.Shared;

namespace BlazorWeb.Server.Mapper
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

