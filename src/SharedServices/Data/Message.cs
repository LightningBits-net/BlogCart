using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace SharedServices.Data
{
    public class Message
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime Timestamp { get; set; }
        public bool IsUserMessage { get; set; } // true if the message is from the user, false if it's from the AI.
        public int ConversationId { get; set; } // Foreign key.
        public virtual Conversation Conversation { get; set; }
    }
}

