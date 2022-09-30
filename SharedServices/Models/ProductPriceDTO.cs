// LightningBits
using System;
using SharedServices.Data;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SharedServices.Models
{
    public class ProductPriceDTO
    {
        public int Id { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public string Size { get; set; }

        public string MyProperty { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Price must be greater than 1")]
        public double Price { get; set; }
    }
}

