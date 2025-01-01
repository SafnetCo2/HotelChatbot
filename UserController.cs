using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelChatbot; // Ensure the namespace is correct for your models

namespace HotelChatbot.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly HotelDbContext _context;

        // Constructor that receives the HotelDbContext via dependency injection
        public UsersController(HotelDbContext context)
        {
            _context = context;
        }

        // GET: api/users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var users = await _context.Users.ToListAsync();
            return Ok(users);  // Returning a 200 OK response
        }

        // GET: api/users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();  // Returning a 404 if the user is not found
            }

            return Ok(user);  // Returning the user object with a 200 OK response
        }

        // POST: api/users
        [HttpPost]
        public async Task<ActionResult<User>> CreateUser(User user)
        {
            // Add user to the database
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // Return a 201 Created response with the created user
            return CreatedAtAction(nameof(GetUser), new { id = user.UserId }, user);
        }

        // PUT: api/users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, User user)
        {
            if (id != user.UserId)
            {
                return BadRequest();  // Returning 400 if IDs do not match
            }

            // Mark the user as modified
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            // Return a 204 No Content response indicating success
            return NoContent();
        }

        // DELETE: api/users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();  // Return 404 if user is not found
            }

            // Remove the user from the database
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            // Return 204 No Content as the response
            return NoContent();
        }
    }
}
