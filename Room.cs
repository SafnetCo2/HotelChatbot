public class Room
{
    public int RoomId { get; set; }
    public string RoomType { get; set; } // Marked as required
    public decimal Price { get; set; }
    public bool IsAvailable { get; set; }

    // Constructor with required properties
    public Room(int roomId, string roomType, decimal price, bool isAvailable)
    {
        RoomId = roomId;
        RoomType = roomType ?? throw new ArgumentNullException(nameof(roomType)); // Ensure non-null
        Price = price;
        IsAvailable = isAvailable;
    }
}
