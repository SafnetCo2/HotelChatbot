// Room.cs
namespace HotelChatbot
{
    public class Room
    {
        public int RoomId { get; set; }
        public string? RoomType { get; set; }
        public decimal Price { get; set; }
        public int Capacity { get; set; }  // Ensure this property is present
    }
}
