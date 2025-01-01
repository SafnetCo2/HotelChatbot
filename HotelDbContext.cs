using Microsoft.EntityFrameworkCore;

namespace HotelChatbot
{
    public class HotelDbContext : DbContext
    {
        // Constructor for DbContextOptions (required for EF migrations)
        public HotelDbContext(DbContextOptions<HotelDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // You can remove this as the connection string should be handled in ConfigureServices
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users");
                entity.HasKey(e => e.UserId);
                entity.Property(e => e.UserId).HasColumnName("UserId").IsRequired();
                entity.Property(e => e.Name).HasColumnName("Name").IsRequired();
                entity.Property(e => e.Email).HasColumnName("Email").IsRequired();
            });

            modelBuilder.Entity<Room>(entity =>
            {
                entity.ToTable("Rooms");
                entity.HasKey(e => e.RoomId);
                entity.Property(e => e.RoomId).HasColumnName("RoomId").IsRequired();
                entity.Property(e => e.RoomType).HasColumnName("RoomType").IsRequired();
                entity.Property(e => e.Price)
                    .HasColumnName("Price")
                    .IsRequired()
                    .HasColumnType("decimal(18,2)");  // Set price column type to decimal with 2 decimal places
                entity.Property(e => e.Capacity).HasColumnName("Capacity").IsRequired();
            });

            modelBuilder.Entity<Booking>(entity =>
            {
                entity.ToTable("Bookings");
                entity.HasKey(e => e.BookingId);
                entity.Property(e => e.BookingId).HasColumnName("BookingId").IsRequired();
                entity.Property(e => e.UserId).HasColumnName("UserId").IsRequired();
                entity.Property(e => e.RoomId).HasColumnName("RoomId").IsRequired();
                entity.Property(e => e.BookingDate).HasColumnName("BookingDate").IsRequired();
            });

            modelBuilder.Entity<ChatMessage>(entity =>
            {
                entity.ToTable("ChatMessages");
                entity.HasKey(e => e.MessageId);
                entity.Property(e => e.MessageId).HasColumnName("MessageId").IsRequired();
                entity.Property(e => e.Message).HasColumnName("Message").IsRequired();
                entity.Property(e => e.Timestamp).HasColumnName("Timestamp").IsRequired();
            });
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<ChatMessage> ChatMessages { get; set; }
    }
}
