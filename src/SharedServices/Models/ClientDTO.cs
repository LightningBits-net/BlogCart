using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

    namespace SharedServices.Models
    {
    public class ClientDTO
    {
        public int ClientId { get; set; }
        public string DomainName { get; set; }
        public DateTime DateCreated { get; set; }
        public string Email { get; set; }
        public int Counter { get; set; }
        public bool IsActive { get; set; }
        public string BillingCycle { get; set; }
        public decimal BillingAmount { get; set; }
        public DateTime BillingStartDate { get; set; }
        public DateTime BillingEndDate { get; set; }
    }
}

