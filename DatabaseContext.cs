using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace HotelChatbot
{
    public class DatabaseContext
    {
        private readonly string _connectionString;

        public DatabaseContext(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection") ??
                throw new ArgumentNullException("DefaultConnection", "Connection string cannot be null.");
        }

        // Fetch all users from the database
        public List<User> GetUsers()
        {
            var users = new List<User>();

            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                const string query = "SELECT * FROM Users";

                using (var cmd = new MySqlCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string name = reader["Name"]?.ToString() ?? throw new ArgumentNullException("Name cannot be null.");
                        string email = reader["Email"]?.ToString() ?? throw new ArgumentNullException("Email cannot be null.");

                        users.Add(new User((int)reader["UserId"], name, email));
                    }
                }
            }

            return users;
        }

        // Fetch all rooms from the database
        public List<Room> GetRooms()
        {
            var rooms = new List<Room>();

            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                const string query = "SELECT * FROM Rooms";

                using (var cmd = new MySqlCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string roomType = reader["RoomType"]?.ToString() ?? throw new ArgumentNullException("RoomType cannot be null.");

                        rooms.Add(new Room(
                            (int)reader["RoomId"],
                            roomType,
                            (decimal)reader["Price"],
                            (bool)reader["IsAvailable"]
                        ));
                    }
                }
            }

            return rooms;
        }

        // Create a booking in the database
        public void CreateBooking(Booking booking)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                const string query = "INSERT INTO Bookings (UserId, RoomId, BookingDate) VALUES (@UserId, @RoomId, @BookingDate)";

                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", booking.UserId);
                    cmd.Parameters.AddWithValue("@RoomId", booking.RoomId);
                    cmd.Parameters.AddWithValue("@BookingDate", booking.BookingDate);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Fetch all chat messages from the database, including User and optional Booking details
        public List<ChatMessage> GetChatMessages()
        {
            var messages = new List<ChatMessage>();

            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                const string query = @"
                    SELECT cm.MessageId, cm.UserId, cm.Message, cm.Timestamp, 
                           u.UserId, u.Name, u.Email,
                           b.BookingId, b.UserId AS BookingUserId, b.RoomId, b.BookingDate
                    FROM ChatMessages cm
                    JOIN Users u ON cm.UserId = u.UserId
                    LEFT JOIN Bookings b ON b.UserId = cm.UserId"; // Optional: Fetch related bookings

                using (var cmd = new MySqlCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // Null check for Message and User details
                        string message = reader["Message"]?.ToString() ?? throw new ArgumentNullException("Message cannot be null.");
                        string userName = reader["Name"]?.ToString() ?? throw new ArgumentNullException("User Name cannot be null.");
                        string userEmail = reader["Email"]?.ToString() ?? throw new ArgumentNullException("User Email cannot be null.");

                        var user = new User(
                            (int)reader["UserId"],
                            userName,
                            userEmail
                        );

                        Booking? booking = null;
                        if (reader["BookingId"] != DBNull.Value)
                        {
                            booking = new Booking(
                                (int)reader["BookingId"],
                                (int)reader["BookingUserId"],
                                (int)reader["RoomId"],
                                (DateTime)reader["BookingDate"]
                            );
                        }

                        messages.Add(new ChatMessage(
                            (int)reader["MessageId"],
                            (int)reader["UserId"],
                            message,
                            (DateTime)reader["Timestamp"],
                            user,
                            booking // Pass the optional Booking object
                        ));
                    }
                }
            }

            return messages;
        }

        // Fetch all hotel staff from the database
        public List<HotelStaff> GetHotelStaff()
        {
            var staffList = new List<HotelStaff>();

            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                const string query = "SELECT * FROM HotelStaff";

                using (var cmd = new MySqlCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string name = reader["Name"]?.ToString() ?? throw new ArgumentNullException("Name cannot be null.");
                        string role = reader["Role"]?.ToString() ?? throw new ArgumentNullException("Role cannot be null.");

                        staffList.Add(new HotelStaff(
                            (int)reader["StaffId"],
                            name,
                            role
                        ));
                    }
                }
            }

            return staffList;
        }
    }
}
