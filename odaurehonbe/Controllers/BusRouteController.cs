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
        private TimeSpan GetStartTime(string timeFrame)
        {
            switch (timeFrame)
            {
                case "0:00 - 6:00": return new TimeSpan(0, 0, 0);
                case "6:00 - 12:00": return new TimeSpan(6, 0, 0);
                case "12:00 - 18:00": return new TimeSpan(12, 0, 0);
                case "18:00 - 0:00": return new TimeSpan(18, 0, 0);
                default: return TimeSpan.Zero;
            }
        }

        private TimeSpan GetEndTime(string timeFrame)
        {
            switch (timeFrame)
            {
                case "0:00 - 6:00": return new TimeSpan(6, 0, 0);
                case "6:00 - 12:00": return new TimeSpan(12, 0, 0);
                case "12:00 - 18:00": return new TimeSpan(18, 0, 0);
                case "18:00 - 0:00": return new TimeSpan(24, 0, 0);
                default: return TimeSpan.Zero;
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetBusRoutes([FromQuery] string? searchQuery, [FromQuery] string[]? timeFrames)
        {
            // Log ra timeFrames để kiểm tra xem có dữ liệu nhận được không
            if (timeFrames != null && timeFrames.Length > 0)
            {
                Console.WriteLine("Received timeFrames: " + string.Join(", ", timeFrames)); // Log giá trị của timeFrames
            }
            else
            {
                Console.WriteLine("No timeFrames received."); // Log khi không có timeFrames
            }

            var query = _dbContext.BusRoutes.AsQueryable();

            if (!string.IsNullOrEmpty(searchQuery))
            {
                query = query.Where(b => b.DepartPlace.Contains(searchQuery) || b.BusRouteID.Equals(searchQuery));
            }

            // Kiểm tra và xử lý timeFrames
            if (timeFrames != null && timeFrames.Length > 0)
            {
                query = query.Where(b =>
                    timeFrames.Any(t =>
                        b.DepartureTime.TimeOfDay >= GetStartTime(t) &&
                        b.DepartureTime.TimeOfDay < GetEndTime(t)
                    )
                );
            }

            var results = await query.ToListAsync();

            // Nếu không có kết quả, trả về thông báo không tìm thấy
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
            // Kiểm tra giá trị id nhận từ URL
            Console.WriteLine($"Received busRouteId: {id}");

            // Kiểm tra nếu tuyến xe không tồn tại
            var existingRoute = await _dbContext.BusRoutes.FindAsync(id);
            if (existingRoute == null) return NotFound();

            // Cập nhật các trường khác mà không thay đổi BusRouteID
            existingRoute.BusRouteID = updatedRoute.BusRouteID;
            existingRoute.DepartPlace = updatedRoute.DepartPlace;
            existingRoute.ArrivalPlace = updatedRoute.ArrivalPlace;
            existingRoute.DepartureTime = updatedRoute.DepartureTime;
            existingRoute.Duration = updatedRoute.Duration;

            // Lưu các thay đổi
            await _dbContext.SaveChangesAsync();

            return NoContent();  // Trả về 204 No Content nếu thành công
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
