using System;
using System.ComponentModel.DataAnnotations;

namespace HotelChatbot
{
    public class ChatMessage
    {
        [Key] // Marking the MessageId as the Primary Key
        public int MessageId { get; set; }
        public int UserId { get; set; }
        public string Message { get; set; }
        public DateTime Timestamp { get; set; }

        // Constructor
        public ChatMessage(int messageId, int userId, string message, DateTime timestamp)
        {
            MessageId = messageId;
            UserId = userId;
            Message = message;
            Timestamp = timestamp;
        }
    }
}
