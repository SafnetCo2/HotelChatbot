using Microsoft.AspNetCore.Mvc;
using HotelChatbot;
using Microsoft.EntityFrameworkCore;

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

        // GET: api/chatmessages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ChatMessage>>> GetChatMessages()
        {
            return await _context.ChatMessages.Include(cm => cm.User).Include(cm => cm.Booking).ToListAsync();
        }

        // GET: api/chatmessages/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ChatMessage>> GetChatMessage(int id)
        {
            var chatMessage = await _context.ChatMessages.FindAsync(id);

            if (chatMessage == null)
            {
                return NotFound();
            }

            return chatMessage;
        }

        // POST: api/chatmessages
        [HttpPost]
        public async Task<ActionResult<ChatMessage>> CreateChatMessage(ChatMessage chatMessage)
        {
            _context.ChatMessages.Add(chatMessage);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetChatMessage), new { id = chatMessage.ChatMessageId }, chatMessage);
        }

        // DELETE: api/chatmessages/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChatMessage(int id)
        {
            var chatMessage = await _context.ChatMessages.FindAsync(id);
            if (chatMessage == null)
            {
                return NotFound();
            }

            _context.ChatMessages.Remove(chatMessage);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
