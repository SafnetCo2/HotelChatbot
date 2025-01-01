using Microsoft.AspNetCore.Mvc;

namespace HotelChatbot.Controllers
{
    [Route("")]
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return Ok("Welcome to the Hotel Chatbot API!");
        }
    }
}
