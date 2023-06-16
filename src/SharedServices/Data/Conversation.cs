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
        public int ClientId { get; set; }
        public string Name { get; set; }
        public string? Context { get; set; }
        public string? SystemMessage { get; set; }

        [ForeignKey("ClientId")]
        public Client Client { get; set; } 

        public virtual ICollection<Message> Messages { get; set; }
    }
}

