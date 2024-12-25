using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using odaurehonbe.Data;
using odaurehonbe.Models;

namespace odaurehonbe.Controllers
{
    [ApiController]
    [Route("api/busroute")]
    public class BusRouteController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public BusRouteController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet]

        public async Task<IActionResult> GetBusRoutes([FromQuery] int? searchQuery)
        {
            var query = _dbContext.BusRoutes.AsQueryable();

            if (searchQuery!=null)
            {
                query = query.Where(b =>b.BusRouteID.Equals(searchQuery));
            }

            var results = await query.ToListAsync();

            if (results.Count == 0)
            {
                return NotFound("No bus routes found matching the criteria.");
            }

            return Ok(results);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBusRoute([FromBody] BusRoute newRoute)
        {
            if (newRoute == null) return BadRequest("Invalid data.");

            _dbContext.BusRoutes.Add(newRoute);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBusRoutes), new { id = newRoute.BusRouteID }, newRoute);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBusRoute(int id, [FromBody] BusRoute updatedRoute)
        {
            Console.WriteLine($"Received busRouteId: {id}");

            var existingRoute = await _dbContext.BusRoutes.FindAsync(id);
            if (existingRoute == null) return NotFound();

            existingRoute.BusRouteID = updatedRoute.BusRouteID;
            existingRoute.DepartPlace = updatedRoute.DepartPlace;
            existingRoute.ArrivalPlace = updatedRoute.ArrivalPlace;
            existingRoute.DepartureTime = updatedRoute.DepartureTime;
            existingRoute.PricePerSeat = updatedRoute.PricePerSeat;
            existingRoute.PricePerSeatVip = updatedRoute.PricePerSeatVip;
            existingRoute.Duration = updatedRoute.Duration;

            await _dbContext.SaveChangesAsync();

            return NoContent(); 
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBusRoute(int id)
        {
            var busRoute = await _dbContext.BusRoutes.FindAsync(id);
            if (busRoute == null) return NotFound();

            _dbContext.BusRoutes.Remove(busRoute);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

    }

}
