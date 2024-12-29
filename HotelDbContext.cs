using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Collections.Generic;

namespace HotelChatbot
{
    public class HotelDbContext : DbContext
    {
        private readonly string? _connectionString; // Make nullable

        public HotelDbContext(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection") ??
                throw new ArgumentNullException("DefaultConnection", "Connection string cannot be null.");
        }

        public HotelDbContext(DbContextOptions<HotelDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<ChatMessage> ChatMessages { get; set; }
        public DbSet<HotelStaff> HotelStaff { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured && _connectionString != null)
            {
                optionsBuilder.UseMySql(_connectionString, ServerVersion.AutoDetect(_connectionString));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public User? GetUserById(int userId) // Make nullable
        {
            return Users.FirstOrDefault(u => u.UserId == userId);
        }

        public Room? GetRoomById(int roomId) // Make nullable
        {
            return Rooms.FirstOrDefault(r => r.RoomId == roomId);
        }

        public Booking? GetBookingById(int bookingId) // Make nullable
        {
            return Bookings.FirstOrDefault(b => b.BookingId == bookingId);
        }

        public List<ChatMessage> GetChatMessagesByUserId(int userId)
        {
            return ChatMessages.Where(cm => cm.UserId == userId).ToList();
        }

        public HotelStaff? GetHotelStaffById(int staffId) // Make nullable
        {
            return HotelStaff.FirstOrDefault(h => h.StaffId == staffId);
        }
    }
}
