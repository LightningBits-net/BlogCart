﻿// LightningBits
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace SharedServices.Data
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool ShopFavorites { get; set; }

        public bool CustomerFavorites { get; set; }

        public string Color { get; set; }

        public string ImageUrl { get; set; }

        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        public ICollection<ProductPrice> ECommerceProductPrices { get; set; }

        //public int ClientId { get; set; }

        ////[ForeignKey("BlogCategoryId")]
        //public Client Client { get; set; }
    }
}

