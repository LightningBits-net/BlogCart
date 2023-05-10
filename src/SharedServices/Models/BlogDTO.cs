using System;
using SharedServices.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SharedServices.Models
{
    public class BlogDTO
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public string Body { get; set; }

        [Required]
        public string Author { get; set; }

        public bool Featured { get; set; }

        public bool BlogFavorite { get; set; }

        public string ImageUrl { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a catogory")]
        public int BlogCategoryId { get; set; }

        public int Views { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime LastUpdated { get; set; }

        [Required]
        public string Status { get; set; }

        public int Rating { get; set; }

        [ForeignKey("BlogCategoryId")]
        public BlogCategory BlogCategory { get; set; }

        public int ClientId { get; set; }

        //[ForeignKey("ClientId")]
        public ClientDTO Client { get; set; }
    }
}

