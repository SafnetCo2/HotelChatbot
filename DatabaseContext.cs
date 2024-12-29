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

        // Method to fetch all users from the database
        public List<User> GetUsers()
        {
            List<User> users = new List<User>();

            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM Users";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Null checks for Name and Email
                            string name = reader["Name"]?.ToString() ?? throw new ArgumentNullException("Name cannot be null.");
                            string email = reader["Email"]?.ToString() ?? throw new ArgumentNullException("Email cannot be null.");

                            users.Add(new User(
                                (int)reader["UserId"],
                                name,
                                email
                            ));
                        }
                    }
                }
            }

            return users;
        }

        // Method to fetch all rooms from the database
        public List<Room> GetRooms()
        {
            List<Room> rooms = new List<Room>();

            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM Rooms";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Null check for RoomType
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
            }

            return rooms;
        }

        // Method to create a booking in the database
        public void CreateBooking(Booking booking)
        {
            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                string query = "INSERT INTO Bookings (UserId, RoomId, BookingDate) VALUES (@UserId, @RoomId, @BookingDate)";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", booking.UserId);
                    cmd.Parameters.AddWithValue("@RoomId", booking.RoomId);
                    cmd.Parameters.AddWithValue("@BookingDate", booking.BookingDate);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Method to fetch all chat messages from the database
        public List<ChatMessage> GetChatMessages()
        {
            List<ChatMessage> messages = new List<ChatMessage>();

            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM ChatMessages";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Null check for Message
                            string message = reader["Message"]?.ToString() ?? throw new ArgumentNullException("Message cannot be null.");

                            messages.Add(new ChatMessage(
                                (int)reader["MessageId"],
                                (int)reader["UserId"],
                                message,
                                (DateTime)reader["Timestamp"]
                            ));
                        }
                    }
                }
            }

            return messages;
        }

        // Method to fetch all hotel staff from the database
        public List<HotelStaff> GetHotelStaff()
        {
            List<HotelStaff> staffList = new List<HotelStaff>();

            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM HotelStaff";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Null checks for Name and Role
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
            }

            return staffList;
        }
    }
}
