using AirlineAPI.Models;
using AirlineAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace AirlineAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookingController : ControllerBase
    {
        //private readonly ILogger _logger;
        private readonly IMessageProducer _messageProducer;

        // In-Memory db
        public static readonly List<Booking> bookings = new List<Booking>();

        public BookingController(/*ILogger logger,*/ IMessageProducer messageProducer)
        {
            //_logger = logger;
            _messageProducer = messageProducer;
        }

        [HttpPost]
        public IActionResult Create(Booking booking)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            bookings.Add(booking);

            _messageProducer.SendingMessages<Booking>(booking);

            return Ok();
        }
    }
}
