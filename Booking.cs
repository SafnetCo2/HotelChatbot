namespace HotelChatbot
{
    public class Booking
    {
        public int BookingId { get; set; }
        public int UserId { get; set; }
        public int RoomId { get; set; }
        public DateTime BookingDate { get; set; }

        // Constructor for Booking
        public Booking(int bookingId, int userId, int roomId, DateTime bookingDate)
        {
            BookingId = bookingId;
            UserId = userId;
            RoomId = roomId;
            BookingDate = bookingDate;
        }
    }
}
