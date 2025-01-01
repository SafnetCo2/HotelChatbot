using Microsoft.AspNetCore.Mvc;
using HotelChatbot;
using Microsoft.EntityFrameworkCore;

namespace HotelChatbot.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly HotelDbContext _context;

        public RoomsController(HotelDbContext context)
        {
            _context = context;
        }

        // GET: api/rooms
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Room>>> GetRooms()
        {
            var rooms = await _context.Rooms.ToListAsync();
            return Ok(rooms);  // Return 200 OK with a list of rooms
        }

        // GET: api/rooms/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Room>> GetRoom(int id)
        {
            var room = await _context.Rooms.FindAsync(id);

            if (room == null)
            {
                return NotFound();  // Return 404 if room not found
            }

            return Ok(room);  // Return 200 OK with the found room
        }

        // POST: api/rooms
        [HttpPost]
        public async Task<ActionResult<Room>> CreateRoom(Room room)
        {
            _context.Rooms.Add(room);
            await _context.SaveChangesAsync();

            // Return a 201 Created response with the created room
            return CreatedAtAction(nameof(GetRoom), new { id = room.RoomId }, room);
        }

        // PUT: api/rooms/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRoom(int id, Room room)
        {
            if (id != room.RoomId)
            {
                return BadRequest();  // Return 400 if IDs don't match
            }

            _context.Entry(room).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            // Return 204 No Content on success
            return NoContent();
        }

        // DELETE: api/rooms/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoom(int id)
        {
            var room = await _context.Rooms.FindAsync(id);
            if (room == null)
            {
                return NotFound();  // Return 404 if room not found
            }

            _context.Rooms.Remove(room);
            await _context.SaveChangesAsync();

            // Return 204 No Content on success
            return NoContent();
        }
    }
}
