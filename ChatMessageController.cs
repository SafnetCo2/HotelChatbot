using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace HotelChatbot.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatMessagesController : ControllerBase
    {
        private readonly HotelDbContext _context;

        public ChatMessagesController(HotelDbContext context)
        {
            _context = context;
        }

        // GET: api/ChatMessages
        [HttpGet]
        public async Task<ActionResult> GetChatMessages()
        {
            var chatMessages = await _context.ChatMessages
                .Include(cm => cm.User)  // Include User data
                .Include(cm => cm.Booking)  // Include Booking data
                .ToListAsync();

            if (chatMessages == null || chatMessages.Count == 0)
            {
                return NotFound("No chat messages found.");
            }

            return Ok(chatMessages);
        }

        // POST: api/ChatMessages
        [HttpPost]
        public async Task<ActionResult<ChatMessage>> PostChatMessage(ChatMessage chatMessage)
        {
            if (chatMessage == null)
            {
                return BadRequest("Chat message cannot be null.");
            }

            if (string.IsNullOrEmpty(chatMessage.Message))
            {
                return BadRequest("Message cannot be empty.");
            }

            // Add the chat message to the context
            _context.ChatMessages.Add(chatMessage);

            // Save changes to the database
            await _context.SaveChangesAsync();

            // Return a 201 response with the location of the newly created resource
            return CreatedAtAction(nameof(GetChatMessages), new { id = chatMessage.MessageId }, chatMessage);
        }

        // DELETE: api/ChatMessages/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChatMessage(int id)
        {
            // Find the chat message by id
            var chatMessage = await _context.ChatMessages.FindAsync(id);

            // If the chat message doesn't exist, return a 404 Not Found response
            if (chatMessage == null)
            {
                return NotFound();
            }

            // Remove the chat message from the database
            _context.ChatMessages.Remove(chatMessage);

            // Save changes to the database
            await _context.SaveChangesAsync();

            // Return a 204 No Content response indicating the message was deleted
            return NoContent();
        }
    }
}
