using System;
using SharedServices.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SharedServices.Models
{
    public class MessageDTO
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime Timestamp { get; set; }
        public bool IsUserMessage { get; set; } // true if the message is from the user, false if it's from the AI.
        public int ConversationId { get; set; } // Foreign key.

        //[ForeignKey("ConversationId")]
        public virtual Conversation Conversation { get; set; }
    }
}

