public class Booking
{
    public int BookingId { get; set; }
    public int UserId { get; set; }
    public int RoomId { get; set; }

    // Ensure BookingDate is set to current UTC time by default
    [JsonConverter(typeof(JsonDateTimeConverter))]
    public DateTime BookingDate { get; set; } = DateTime.UtcNow;

    public Booking(int userId, int roomId)
    {
        UserId = userId;
        RoomId = roomId;
    }
}
