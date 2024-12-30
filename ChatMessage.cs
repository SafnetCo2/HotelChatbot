namespace HotelChatbot
{
    public class ChatMessage
    {
        public int MessageId { get; set; }
        public int UserId { get; set; }
        public string? Message { get; set; }  // Made nullable
        public DateTime Timestamp { get; set; }

        public User? User { get; set; }
        public Booking? Booking { get; set; }

        // Parameterless constructor for EF Core
        public ChatMessage()
        {
            Message = string.Empty;  // Initialize Message with a default value
        }
    }
}
