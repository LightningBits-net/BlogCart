// LightningBits
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SharedServices.Models
{
    public class ProductDTO
    {
        
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public bool ShopFavorites { get; set; }

        public bool CustomerFavorites { get; set; }

        public string Color { get; set; }

        public string ImageUrl { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please select a catogory")]
        public int CategoryId { get; set; }
       

        public CategoryDTO Category { get; set; }

    }
}

