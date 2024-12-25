using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using odaurehonbe.Data;

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
        public async Task<IActionResult> GetBusRoutes([FromQuery] string? searchQuery, [FromQuery] List<string>? timeFrames)
        {
            // Log timeFrames nhận từ frontend
            Console.WriteLine($"Received timeFrames: {string.Join(", ", values: timeFrames)}");

            var busRoutes = await _dbContext.BusRoutes.ToListAsync();

            if (!string.IsNullOrEmpty(searchQuery))
            {
                busRoutes = busRoutes.Where(br => br.BusRouteID.ToString().Contains(searchQuery)).ToList();
            }

            if (timeFrames != null && timeFrames.Any())
            {
                busRoutes = busRoutes.Where(br =>
                {
                    var hour = br.DepartureTime.Hour;

                    // Log giờ trong DepartureTime
                    Console.WriteLine($"BusRoute ID: {br.BusRouteID}, DepartureTime: {br.DepartureTime}, Hour: {hour}");

                    return timeFrames.Any(tf =>
                    {
                        var parts = tf.Split(" - ");
                        int start = int.Parse(parts[0].Split(':')[0]);
                        int end = parts[1] == "0:00" ? 24 : int.Parse(parts[1].Split(':')[0]);

                        // Log thời gian start và end
                        Console.WriteLine($"Checking time frame: {start} - {end} for hour {hour}");

                        if (end == 24)
                        {
                            return hour >= start || hour < end;
                        }
                        return hour >= start && hour < end;
                    });
                }).ToList();
            }

            return Ok(busRoutes);
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
