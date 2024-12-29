using System.ComponentModel.DataAnnotations; // For Key attribute
using System; // For DateTime
using HotelChatbot;
public class ChatMessage
{
    public int ChatMessageId { get; set; }
    public int MessageId { get; set; }
    public int UserId { get; set; }
    public string Message { get; set; }
    public DateTime Timestamp { get; set; }
    public User User { get; set; }  // Assuming you want to associate the User

    // Assuming you're trying to relate ChatMessage with Booking
    public Booking? Booking { get; set; }

    // Constructor
    public ChatMessage(int messageId, int userId, string message, DateTime timestamp, User user, Booking? booking)
    {
        MessageId = messageId;
        UserId = userId;
        Message = message;
        Timestamp = timestamp;
        User = user ?? throw new ArgumentNullException(nameof(user));  // Ensure User is always passed
        Booking = booking;
    }
}
