using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using odaurehonbe.Data;
using odaurehonbe.Models;

namespace odaurehonbe.Controllers
{
    [Route("api/bus")]
    [ApiController]
    public class BusesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BusesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/buses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bus>>> GetBuses()
        {
            return await _context.Buses.Include(b => b.BusDrivers)
                                        .Include(b => b.BusBusRoutes)
                                        .ToListAsync();
        }

        // GET: api/buses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Bus>> GetBus(int id)
        {
            var bus = await _context.Buses.Include(b => b.BusDrivers)
                                          .Include(b => b.BusBusRoutes)
                                          .FirstOrDefaultAsync(b => b.BusID == id);

            if (bus == null)
            {
                return NotFound();
            }

            return bus;
        }
        [HttpPost]
        public async Task<IActionResult> AddBus([FromBody] BusRequestModel busRequest)
        {
            if (busRequest == null)
            {
                return BadRequest("Dữ liệu không hợp lệ.");
            }

            // Chuyển các chuỗi busRouteIds và driverIds thành mảng số
            var busRouteIds = busRequest.BusRouteIds
                .Split(',')
                .Select(id => int.Parse(id.Trim()))
                .ToList();

            var driverIds = busRequest.DriverIds
                .Split(',')
                .Select(id => int.Parse(id.Trim()))
                .ToList();

            // Kiểm tra xem BusID đã tồn tại chưa
            var existingBus = await _context.Buses.FindAsync(busRequest.BusID);
            if (existingBus != null)
            {
                return BadRequest("BusID đã tồn tại.");
            }

            // Tạo và lưu trữ đối tượng Bus mới
            var newBus = new Bus
            {
                BusID = busRequest.BusID,
                NumSeat = busRequest.NumSeat,
                PlateNum = busRequest.PlateNum,
                Type = busRequest.Type,
            };

            // Thêm Bus vào cơ sở dữ liệu
            _context.Buses.Add(newBus);

            // Lưu trữ BusBusRoutes và BusDrivers
            foreach (var routeId in busRouteIds)
            {
                // Kiểm tra xem BusRoute có tồn tại trong cơ sở dữ liệu không
                var busRoute = await _context.BusRoutes.FindAsync(routeId);
                if (busRoute == null)
                {
                    return BadRequest($"BusRoute với ID {routeId} không tồn tại.");
                }

                _context.BusBusRoutes.Add(new BusBusRoute { BusID = busRequest.BusID, BusRouteID = routeId });
            }

            foreach (var driverId in driverIds)
            {
                // Kiểm tra xem Driver có tồn tại trong cơ sở dữ liệu không
                var driver = await _context.Drivers.FindAsync(driverId);
                if (driver == null)
                {
                    return BadRequest($"Driver với ID {driverId} không tồn tại.");
                }

                _context.BusDrivers.Add(new BusDriver { BusID = busRequest.BusID, DriverID = driverId });
            }

            // Lưu các thay đổi
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBus), new { id = busRequest.BusID }, busRequest);
        }




        [HttpPut("{id}")]

        public async Task<IActionResult> PutBus(int id, BusRequestModel busRequest)
        {
            if (id != busRequest.BusID)
            {
                return BadRequest("Bus ID mismatch.");
            }

            // Tìm chiếc xe trong cơ sở dữ liệu
            var existingBus = await _context.Buses
                                             .Include(b => b.BusDrivers)
                                             .Include(b => b.BusBusRoutes)  // Bao gồm các tuyến đường liên quan
                                             .FirstOrDefaultAsync(b => b.BusID == id);

            if (existingBus == null)
            {
                return NotFound("Bus not found.");
            }

            // Cập nhật thông tin xe
            existingBus.NumSeat = busRequest.NumSeat;
            existingBus.PlateNum = busRequest.PlateNum;
            existingBus.Type = busRequest.Type;

            // Cập nhật tuyến đường (bus route) nếu có
            var existingBusRouteIds = existingBus.BusBusRoutes.Select(br => br.BusRouteID).ToList();
            var busRouteIds = busRequest.BusRouteIds.Split(',').Select(int.Parse).ToList();

            // Xóa các tuyến đường không còn liên quan đến chiếc xe hiện tại
            foreach (var existingRouteId in existingBusRouteIds)
            {
                if (!busRouteIds.Contains(existingRouteId))
                {
                    var busRoute = existingBus.BusBusRoutes.FirstOrDefault(br => br.BusRouteID == existingRouteId);
                    if (busRoute != null)
                    {
                        existingBus.BusBusRoutes.Remove(busRoute); // Xóa tuyến đường không còn liên quan
                    }
                }
            }

            // Thêm các tuyến đường mới (nếu có)
            foreach (var routeId in busRouteIds)
            {
                if (!existingBusRouteIds.Contains(routeId)) // Nếu tuyến đường chưa có
                {
                    var busRoute = await _context.BusBusRoutes.FindAsync(routeId);
                    if (busRoute != null)
                    {
                        existingBus.BusBusRoutes.Add(busRoute); // Thêm tuyến đường mới
                    }
                }
            }

            // Cập nhật các lái xe (drivers)
            var existingDriverIds = existingBus.BusDrivers.Select(bd => bd.DriverID).ToList();
            var driverIds = busRequest.DriverIds.Split(',').Select(int.Parse).ToList();

            // Xóa các lái xe không còn liên quan đến chiếc xe hiện tại
            foreach (var existingDriverId in existingDriverIds)
            {
                if (!driverIds.Contains(existingDriverId))
                {
                    var busDriver = existingBus.BusDrivers.FirstOrDefault(bd => bd.DriverID == existingDriverId);
                    if (busDriver != null)
                    {
                        existingBus.BusDrivers.Remove(busDriver); // Xóa lái xe không còn liên quan
                    }
                }
            }

            // Thêm các lái xe mới (nếu có)
            foreach (var driverId in driverIds)
            {
                if (!existingDriverIds.Contains(driverId)) // Nếu lái xe chưa có
                {
                    existingBus.BusDrivers.Add(new BusDriver { BusID = existingBus.BusID, DriverID = driverId }); // Thêm lái xe mới
                }
            }

            // Lưu thay đổi vào cơ sở dữ liệu
            await _context.SaveChangesAsync();

            return NoContent(); // Trả về mã trạng thái NoContent (204) khi cập nhật thành công
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBus(int id)
        {
            var bus = await _context.Buses
                                    .Include(b => b.BusDrivers)
                                    .Include(b => b.BusBusRoutes)  // Bao gồm các tuyến đường liên quan
                                    .FirstOrDefaultAsync(b => b.BusID == id);

            if (bus == null)
            {
                return NotFound("Bus not found.");
            }

            // Xóa các liên kết trong BusBusRoutes (tuyến đường liên quan)
            _context.BusBusRoutes.RemoveRange(bus.BusBusRoutes);

            // Xóa các liên kết trong BusDrivers (lái xe liên quan)
            _context.BusDrivers.RemoveRange(bus.BusDrivers);

            // Xóa chiếc xe
            _context.Buses.Remove(bus);

            // Lưu thay đổi vào cơ sở dữ liệu
            await _context.SaveChangesAsync();

            return NoContent(); // Trả về mã trạng thái NoContent (204) khi xóa thành công
        }

    }
}
