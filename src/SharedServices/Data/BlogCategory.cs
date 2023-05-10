using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SharedServices.Data
{
    public class BlogCategory
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime CreateDate { get; set; }

        public int ClientId { get; set; }

        [ForeignKey("ClientId")]
        public Client Client { get; set; }
    }
}

