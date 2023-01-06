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

        [Required(ErrorMessage = "Please enter a color or *")]
        public string Color { get; set; }

        public string ImageUrl { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a catogory")]
        public int CategoryId { get; set; }
       
        public CategoryDTO Category { get; set; }

        public ICollection<ProductPriceDTO> ECommerceProductPrices { get; set; }
    }
}

