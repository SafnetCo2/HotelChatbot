namespace HotelChatbot
{
    public class HotelStaff
    {
        public int StaffId { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }

        // Constructor for HotelStaff
        public HotelStaff(int staffId, string name, string role)
        {
            StaffId = staffId;
            Name = name;
            Role = role;
        }
    }
}
