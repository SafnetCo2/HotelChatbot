using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace HotelChatbot
{
    public class DatabaseContext
    {
        private readonly string _connectionString;
        private readonly ILogger<DatabaseContext> _logger;

        public DatabaseContext(IConfiguration configuration, ILogger<DatabaseContext> logger)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection") ??
                throw new ArgumentNullException("DefaultConnection", "Connection string cannot be null.");
            _logger = logger ?? throw new ArgumentNullException(nameof(logger), "Logger cannot be null.");
        }

        // Fetch all users from the database
        public List<Dictionary<string, object>> GetUsers()
        {
            var users = new List<Dictionary<string, object>>();
            try
            {
                using (var conn = new MySqlConnection(_connectionString))
                {
                    conn.Open();
                    const string query = "SELECT * FROM Users";

                    using (var cmd = new MySqlCommand(query, conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var user = new Dictionary<string, object>();
                            user.Add("UserId", reader["UserId"]);
                            user.Add("Name", reader["Name"]);
                            user.Add("Email", reader["Email"]);
                            users.Add(user);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching users from the database.");
                throw;
            }

            return users;
        }

        // Fetch all rooms from the database
        public List<Dictionary<string, object>> GetRooms()
        {
            var rooms = new List<Dictionary<string, object>>();
            try
            {
                using (var conn = new MySqlConnection(_connectionString))
                {
                    conn.Open();
                    const string query = "SELECT * FROM Rooms";

                    using (var cmd = new MySqlCommand(query, conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var room = new Dictionary<string, object>();
                            room.Add("RoomId", reader["RoomId"]);
                            room.Add("RoomType", reader["RoomType"]);
                            room.Add("Price", reader["Price"]);
                            room.Add("Capacity", reader["Capacity"]);
                            rooms.Add(room);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching rooms from the database.");
                throw;
            }

            return rooms;
        }

        // Create a booking in the database
        public void CreateBooking(int userId, int roomId, DateTime bookingDate)
        {
            try
            {
                using (var conn = new MySqlConnection(_connectionString))
                {
                    conn.Open();
                    const string query = "INSERT INTO Bookings (UserId, RoomId, BookingDate) VALUES (@UserId, @RoomId, @BookingDate)";

                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserId", userId);
                        cmd.Parameters.AddWithValue("@RoomId", roomId);
                        cmd.Parameters.AddWithValue("@BookingDate", bookingDate);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating a booking in the database.");
                throw;
            }
        }

        // Fetch all chat messages from the database
        public List<Dictionary<string, object>> GetChatMessages()
        {
            var messages = new List<Dictionary<string, object>>();
            try
            {
                using (var conn = new MySqlConnection(_connectionString))
                {
                    conn.Open();
                    const string query = "SELECT * FROM ChatMessages";

                    using (var cmd = new MySqlCommand(query, conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var message = new Dictionary<string, object>();
                            message.Add("MessageId", reader["MessageId"]);
                            message.Add("UserId", reader["UserId"]);
                            message.Add("Message", reader["Message"]);
                            message.Add("Timestamp", reader["Timestamp"]);
                            messages.Add(message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching chat messages from the database.");
                throw;
            }

            return messages;
        }
    }
}
