// LightningBits
using System;
using AutoMapper;
using SharedServices.Data;
using SharedServices.Models;
using SharedServices.ViewModels;

namespace SharedServices.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<ProductPrice, ProductPriceDTO>().ReverseMap();
            CreateMap<OrderDetail, OrderDetailDTO>().ReverseMap();
            CreateMap<OrderHeaderDTO, OrderHeader>().ReverseMap();
            CreateMap<OrderDTO, Order>().ReverseMap();
            CreateMap<ToDoItemDTO, ToDoItem>().ReverseMap();
        }
    }
       
}

