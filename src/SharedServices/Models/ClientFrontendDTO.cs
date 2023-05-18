using System;
namespace SharedServices.Models
{
	public class ClientFrontendDTO
	{
        public int ClientId { get; set; }
        public string DomainName { get; set; }
        public float Counter { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
    }
}

