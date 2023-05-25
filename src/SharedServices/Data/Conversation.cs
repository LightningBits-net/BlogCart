using System;
using SharedServices.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SharedServices.Models;

namespace SharedServices.Data
{
    public class Conversation
    {
        public int Id { get; set; }
        public string Name { get; set; } // Optional field to identify a conversation.
        public int ClientId { get; set; } // Foreign key for the client.

        [ForeignKey("ClientId")]
        public Client Client { get; set; } // Navigation property.

        public virtual ICollection<Message> Messages { get; set; }
    }
}

