using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using odaurehonbe.Data;
using System.Threading.Tasks;
using System.Linq;
using odaurehonbe.Models;

namespace odaurehonbe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TicketController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("bus-routes")]
        public async Task<IActionResult> GetBusRoutes()
        {
            var routes = await _context.BusRoutes
                .Include(br => br.BusBusRoutes)
                .ThenInclude(bbr => bbr.Bus)
                .Select(br => new
                {
                    br.BusRouteID,
                    br.DepartPlace,
                    br.ArrivalPlace,
                    br.DepartureTime,
                    br.Duration,
                    Buses = br.BusBusRoutes.Select(bbr => new
                    {
                        bbr.Bus.BusID,
                        bbr.Bus.NumSeat,
                        bbr.Bus.PlateNum,
                        bbr.Bus.Type,
                         bbr.Bus.SeatsAvailable,
                        bbr.Bus.PricePerSeat,

                    })
                })
                .ToListAsync();

            return Ok(routes);
        }


        [HttpGet("bus-routes/{busId}")]
        public async Task<IActionResult> GetBusRouteByBusId(int busId)
        {
            var busRoute = await _context.BusBusRoutes
                .Where(bbr => bbr.BusID == busId)
                .Include(bbr => bbr.BusRoute)
                .Include(bbr => bbr.Bus)
                .ThenInclude(bus => bus.Seats)  
                .Select(bbr => new
                {
                    bbr.BusRoute.BusRouteID,
                    bbr.BusRoute.DepartPlace,
                    bbr.BusRoute.ArrivalPlace,
                    bbr.BusRoute.DepartureTime,
                    bbr.BusRoute.Duration,
                    Bus = new
                    {
                        bbr.Bus.BusID,
                        bbr.Bus.NumSeat,
                        bbr.Bus.PlateNum,
                        bbr.Bus.Type,
                        bbr.Bus.SeatsAvailable,
                        bbr.Bus.PricePerSeat,
                        Seats = bbr.Bus.Seats.Select(seat => new
                        {
                            seat.SeatID,
                            seat.SeatNumber,
                            seat.IsBooked
                        }).ToList()  
                    }
                })
                .FirstOrDefaultAsync();

            if (busRoute == null)
            {
                return NotFound($"Bus with ID {busId} not found.");
            }

            return Ok(busRoute);
        }
        [HttpPost("create-tickets")]
        public IActionResult CreateTickets([FromBody] TicketRequestModel request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var seatNumbers = request.SeatNum
                    .Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)  
                    .ToList();

                var tickets = new List<Ticket>();

                foreach (var seatNumber in seatNumbers)
                {
                    var ticket = new Ticket
                    {
                        BusID = request.BusID,
                        CustomerID = request.CustomerID,
                        SeatNum = seatNumber,
                        Type = request.Type,
                        Price = request.Price,
                        BookingDate = DateTime.UtcNow,
                        Status = "Pending" 
                    };

                    tickets.Add(ticket);
                }

                _context.Tickets.AddRange(tickets);
                _context.SaveChanges();

                return Ok(new { message = "Tickets created successfully.", tickets });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "An error occurred while creating tickets.", details = ex.Message });
            }
        }

    }


}

