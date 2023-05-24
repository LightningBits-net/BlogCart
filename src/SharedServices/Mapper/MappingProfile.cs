// LightningBits
using System;
using AutoMapper;
using Microsoft.VisualBasic;
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
            CreateMap<BlogCategory, BlogCategoryDTO>().ReverseMap();
            CreateMap<Blog, BlogDTO>().ReverseMap();
            CreateMap<Client, ClientDTO>().ReverseMap();
            CreateMap<Client, ClientFrontendDTO>().ReverseMap();
            CreateMap<Conversation, ConversationDTO>().ReverseMap();
            CreateMap<Message, MessageDTO>().ReverseMap();
        }
    }
       
}

