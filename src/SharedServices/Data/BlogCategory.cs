using System;
using System.ComponentModel.DataAnnotations;

namespace SharedServices.Data
{
    public class BlogCategory
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime CreateDate { get; set; }

        //public int ClientId { get; set; }

        ////[ForeignKey("BlogCategoryId")]
        //public Client Client { get; set; }
    }
}

