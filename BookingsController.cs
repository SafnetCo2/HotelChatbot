using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using HotelChatbot;

namespace HotelChatbot.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly HotelDbContext _context;

        public BookingsController(HotelDbContext context)
        {
            _context = context;
        }

        // GET api/bookings
        [HttpGet]
        public ActionResult<List<Booking>> GetBookings()
        {
            var bookings = _context.Bookings.ToList();
            return Ok(bookings);
        }

        // GET api/bookings/{id}
        [HttpGet("{id}")]
        public ActionResult<Booking> GetBooking(int id)
        {
            var booking = _context.Bookings.FirstOrDefault(b => b.BookingId == id);
            if (booking == null)
            {
                return NotFound();
            }
            return Ok(booking);
        }

        // POST api/bookings
        [HttpPost]
        public ActionResult<Booking> CreateBooking([FromBody] Booking booking)
        {
            _context.Bookings.Add(booking);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetBooking), new { id = booking.BookingId }, booking);
        }

        // PUT api/bookings/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateBooking(int id, [FromBody] Booking booking)
        {
            var existingBooking = _context.Bookings.FirstOrDefault(b => b.BookingId == id);
            if (existingBooking == null)
            {
                return NotFound();
            }

            existingBooking.UserId = booking.UserId;
            existingBooking.RoomId = booking.RoomId;
            existingBooking.BookingDate = booking.BookingDate;

            _context.SaveChanges();
            return NoContent();
        }

        // DELETE api/bookings/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteBooking(int id)
        {
            var booking = _context.Bookings.FirstOrDefault(b => b.BookingId == id);
            if (booking == null)
            {
                return NotFound();
            }

            _context.Bookings.Remove(booking);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
