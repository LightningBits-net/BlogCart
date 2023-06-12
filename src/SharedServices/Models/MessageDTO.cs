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
            public bool IsUserMessage { get; set; }
            public int ConversationId { get; set; }
            public bool IsFav { get; set; }

            public virtual Conversation Conversation { get; set; }
        }
    }

