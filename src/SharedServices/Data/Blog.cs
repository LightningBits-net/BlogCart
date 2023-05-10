using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace SharedServices.Data
{
    public class Blog
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Body { get; set; }

        public string Author { get; set; }

        public bool Featured { get; set; }

        public bool BlogFavorite { get; set; }

        public string ImageUrl { get; set; }

        public int Views { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime LastUpdated { get; set; }

        public string Status { get; set; }

        public int Rating { get; set; }

        public int BlogCategoryId { get; set; }

        [ForeignKey("BlogCategoryId")]
        public BlogCategory BlogCategory { get; set; }

        //public int ClientId { get; set; }

        ////[ForeignKey("BlogCategoryId")]
        //public Client Client { get; set; }
    }
}
