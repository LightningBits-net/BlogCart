﻿using System;
using SharedServices.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SharedServices.Models
{
    public class ConversationDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ClientId { get; set; } // Corresponding field in the DTO.

        //[ForeignKey("ClientId")]
        public Client Client { get; set; } // Navigation property.

        public virtual ICollection<MessageDTO> Messages { get; set; }
    }
}
