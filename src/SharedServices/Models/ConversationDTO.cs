using System;
using SharedServices.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SharedServices.Models
{
    public class ConversationDTO
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public string Name { get; set; }
        public string? Context { get; set; }
        public string? SystemMessage { get; set; }

        public Client Client { get; set; } 

        public virtual ICollection<MessageDTO> Messages { get; set; }
    }
}

