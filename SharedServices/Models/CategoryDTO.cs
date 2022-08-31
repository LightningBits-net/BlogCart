// LightningBits
using System;
using System.ComponentModel.DataAnnotations;

namespace SharedServices
{
    public class CategoryDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter name")]
        public string? Name { get; set; }

    }
}

