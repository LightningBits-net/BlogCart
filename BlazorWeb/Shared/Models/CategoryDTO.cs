// LightningBits
using System;
using System.ComponentModel.DataAnnotations;

namespace BlazorWeb.Shared
{
    public class CategoryDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter namey")]
        public string? Name { get; set; }

    }
}

