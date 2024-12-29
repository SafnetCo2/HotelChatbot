namespace HotelChatbot
{
    public class User
    {
        public int UserId { get; set; }
        public string? Name { get; set; }  // Nullable Name property
        public string? Email { get; set; } // Nullable Email property

        // Constructor with non-null validation
        public User(int userId, string? name, string? email)
        {
            UserId = userId;
            Name = name; // Can be null
            Email = email; // Can be null
        }
    }
}
