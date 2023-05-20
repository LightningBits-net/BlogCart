using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SharedServices.Models
{
    public class ClientDTO
    {
        public int ClientId { get; set; }

        [Required]
        public string Name { get; set; }

        public string Address { get; set; }

        [Required]
        public string DomainName { get; set; }

        public DateTime? DateCreated { get; set; }

        public string Description { get; set; }

        [Required]
        public string Email { get; set; }

        public float Counter { get; set; }

        public string ImageUrl { get; set; }

        //public bool? IsActive { get; set; }

        //public string BillingCycle { get; set; }

        //[Column(TypeName = "decimal(18,2)")]
        //public decimal? BillingAmount { get; set; }

        //public DateTime? BillingStartDate { get; set; }

        //public DateTime? BillingEndDate { get; set; }
    }
}

