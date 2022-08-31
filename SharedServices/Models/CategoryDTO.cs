// LightningBits
using System;
using System.ComponentModel.DataAnnotations;

namespace BlazorWeb.Shared
{
    public class CategoryDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter name")]
        public string? Name { get; set; }

    }
}

