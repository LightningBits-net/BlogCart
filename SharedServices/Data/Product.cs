// LightningBits
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace SharedServices
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

        public int CatgoryId { get; set; }
        [ForeignKey("CategoryId")]

        public Category Category { get; set; }

    }
}

