// LightningBits
using System;
using System.ComponentModel.DataAnnotations;

namespace SharedServices
{
    public class Category
    {
        [Key]
       public int Id { get; set; }

         public string? Name { get; set; }

            public DateTime CreateDate { get; set; }
    }
}

