using System;
using System.ComponentModel.DataAnnotations;
using SharedServices.Models;

namespace ECommerce_Client.ViewModels
{
    public class DetailsVM
    {
        public DetailsVM()
        {
            ProductPrice = new();
            Count = 1;
        }
            
        [Range(1, int.MaxValue, ErrorMessage ="Please enter a vlue greater than 0")]
       public int Count { get; set; }

        [Required]
       public int SelectedProductPriceId { get; set; }

        public ProductPriceDTO ProductPrice { get; set; }
    }
}

