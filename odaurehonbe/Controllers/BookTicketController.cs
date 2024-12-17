using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using odaurehonbe.Data;
using System.Threading.Tasks;
using System.Linq;
using odaurehonbe.Models;

namespace odaurehonbe.Controllers
{
    [Route("api/bookticket")]
    [ApiController]
    public class BookTicketController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BookTicketController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet("bus-bus-routes")]
        public async Task<IActionResult> GetBusBusRoutes()
        {
            try
            {
                var busBusRoutes = await _context.BusBusRoutes
                    .Include(bbr => bbr.BusRoute) 
                    .Include(bbr => bbr.Seats)   
                    .Select(bbr => new
                    {
                        bbr.BusRouteID,
                        DepartPlace = bbr.BusRoute.DepartPlace,
                        ArrivalPlace = bbr.BusRoute.ArrivalPlace,
                        DepartureTime = bbr.BusRoute.DepartureTime,
                        Duration = bbr.BusRoute.Duration,
                        PricePerSeat = bbr.Bus.Type == "VIP"
                            ? bbr.BusRoute.PricePerSeatVip
                            : bbr.BusRoute.PricePerSeat,
                        Bus = new
                        {
                            bbr.BusID,
                            bbr.Bus.NumSeat,
                            bbr.Bus.PlateNum,
                            bbr.Bus.Type
                        },
                        SeatsAvailable = bbr.Seats != null
                            ? bbr.Seats.Count(seat => !seat.IsBooked)
                            : 0,
                        Seats = bbr.Seats != null
                            ? bbr.Seats.Select(seat => new
                            {
                                seat.SeatID,
                                seat.SeatNumber,
                                seat.IsBooked
                            }).ToList()
                            : null 
                    })
                    .ToListAsync();

                return Ok(busBusRoutes);
            }
            catch (Exception ex)
            {
             
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    Message = "An error occurred while processing your request.",
                    Error = ex.Message
                });
            }
        }




        //[HttpGet("bus-routes/{busId}")]
        //public async Task<IActionResult> GetBusRouteByBusId(int busId)
        //{
        //    var busRoute = await _context.BusBusRoutes
        //        .Where(bbr => bbr.BusID == busId)
        //        .Include(bbr => bbr.BusRoute)
        //        .Include(bbr => bbr.Bus)
        //        .ThenInclude(bus => bus.Seats)  
        //        .Select(bbr => new
        //        {
        //            bbr.BusRoute.BusRouteID,
        //            bbr.BusRoute.DepartPlace,
        //            bbr.BusRoute.ArrivalPlace,
        //            bbr.BusRoute.DepartureTime,
        //            bbr.BusRoute.Duration,
        //            bbr.BusRoute.PricePerSeat,

        //            Bus = new
        //            {
        //                bbr.Bus.BusID,
        //                bbr.Bus.NumSeat,
        //                bbr.Bus.PlateNum,
        //                bbr.Bus.Type,
        //                bbr.Bus.SeatsAvailable,
        //                Seats = bbr.Bus.Seats.Select(seat => new
        //                {
        //                    seat.SeatID,
        //                    seat.SeatNumber,
        //                    seat.IsBooked
        //                }).ToList()  
        //            }
        //        })
        //        .FirstOrDefaultAsync();

        //    if (busRoute == null)
        //    {
        //        return NotFound($"Bus with ID {busId} not found.");
        //    }

        //    return Ok(busRoute);
        //}
        //[HttpPost("create-tickets")]
        //public IActionResult CreateTickets([FromBody] TicketRequestModel request)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    try
        //    {
        //        var seatNumbers = request.SeatNum
        //            .Split(',', StringSplitOptions.RemoveEmptyEntries)
        //            .Select(int.Parse)  
        //            .ToList();

        //        var tickets = new List<Ticket>();

        //        foreach (var seatNumber in seatNumbers)
        //        {
        //            var ticket = new Ticket
        //            {
        //                BusID = request.BusID,
        //                CustomerID = request.CustomerID,
        //                SeatNum = seatNumber,
        //                Type = request.Type,
        //                Price = request.Price,
        //                BookingDate = DateTime.UtcNow,
        //                Status = "Pending" 
        //            };

        //            tickets.Add(ticket);
        //        }

        //        _context.Tickets.AddRange(tickets);
        //        _context.SaveChanges();

        //        return Ok(new { message = "Tickets created successfully.", tickets });
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, new { error = "An error occurred while creating tickets.", details = ex.Message });
        //    }
        //}
        [HttpGet("bus-routes/{busId}")]
        public async Task<IActionResult> GetBusRouteByBusId(int busId)
        {
            try
            {
                var busRoute = await _context.BusBusRoutes
                    .Where(bbr => bbr.BusID == busId)
                    .Include(bbr => bbr.BusRoute)  
                    .Include(bbr => bbr.Bus)  
                    .Include(bbr => bbr.Seats)  
                    .Select(bbr => new
                    {
                        bbr.BusRouteID,
                        DepartPlace = bbr.BusRoute.DepartPlace,
                        ArrivalPlace = bbr.BusRoute.ArrivalPlace,
                        DepartureTime = bbr.BusRoute.DepartureTime,
                        Duration = bbr.BusRoute.Duration,
                        Price = bbr.Bus.Type == "VIP" ? bbr.BusRoute.PricePerSeatVip : bbr.BusRoute.PricePerSeat,
                        Bus = new
                        {
                            bbr.BusID,
                            bbr.Bus.NumSeat,
                            bbr.Bus.PlateNum,
                            bbr.Bus.Type
                        },
                        SeatsAvailable = bbr.Seats != null
                            ? bbr.Seats.Count(seat => !seat.IsBooked) 
                            : 0,
                        Seats = bbr.Seats != null
                            ? bbr.Seats.Select(seat => new
                            {
                                seat.SeatID,
                                seat.SeatNumber,
                                seat.IsBooked
                            }).ToList()
                            : null
                    })
                    .FirstOrDefaultAsync(); 
                if (busRoute == null)
                {
                    return NotFound($"Bus with ID {busId} not found.");
                }

                return Ok(busRoute);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    Message = "An error occurred while processing your request.",
                    Error = ex.Message
                });
            }
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
                    var seat = _context.Seats
                        .Include(s => s.BusBusRoute)
                        .FirstOrDefault(s => s.BusBusRoute.BusID == request.BusID && s.SeatID == seatNumber); // Convert seatNumber to string

                    if (seat == null || seat.IsBooked)
                    {
                        return BadRequest(new { error = $"Seat {seatNumber} is not available." });
                    }

                    var ticket = new Ticket
                    {
                        BusID = request.BusID,
                        CustomerID = request.CustomerID,
                        SeatNum = seatNumber,
                        Type = request.Type,
                        Price = request.Price,
                        BookingDate = DateTime.UtcNow,
                        Status = "Chờ thanh toán"
                    };

                    tickets.Add(ticket);

                    seat.IsBooked = true;
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

