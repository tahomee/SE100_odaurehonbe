using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using odaurehonbe.Data;
using odaurehonbe.Models;

namespace odaurehonbe.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public AuthController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] Customer customer)
        {
            if (string.IsNullOrEmpty(customer.Name) ||
                string.IsNullOrEmpty(customer.PhoneNumber) ||
                string.IsNullOrEmpty(customer.Account?.Password) ||
                string.IsNullOrEmpty(customer.Account?.UserName) ||
                string.IsNullOrEmpty(customer.Account?.UserType))
            {
                return BadRequest(new { message = "Please fill in all the required fields." });
            }

            // Kiểm tra tài khoản đã tồn tại
            if (_dbContext.Accounts.Any(a => a.UserName == customer.Account.UserName))
            {
                return BadRequest(new { message = "Username already exists." });
            }

            // Tạo bản ghi Account
            var account = new Account
            {
                UserName = customer.Account.UserName,
                Password = customer.Account.Password,
                UserType = customer.Account.UserType,
                Status = "Active" // Hoặc trạng thái mặc định
            };

            _dbContext.Accounts.Add(account);
            await _dbContext.SaveChangesAsync();

            // Liên kết Customer với Account
            customer.AccountID = account.AccountID; // Gán AccountID từ tài khoản đã tạo
            _dbContext.Customers.Add(customer);

            await _dbContext.SaveChangesAsync();

            return Ok(new { message = "Registration successful!" });
        }

        [HttpPost("signin")]
        public async Task<IActionResult> SignIn([FromBody] SignInRequest request)
        {
            if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
            {
                return BadRequest(new { message = "Email and password are required." });
            }

            // Xác thực tài khoản
            var account = await _dbContext.Accounts
                .FirstOrDefaultAsync(a => a.UserName == request.Email && a.Password == request.Password);

            if (account == null)
            {
                return Unauthorized(new { message = "Invalid email or password." });
            }

            // Trả về thông tin loại tài khoản
            return Ok(new
            {
                message = "Login successful!",
                accountType = account.UserType,
                accountId = account.AccountID // Thêm thông tin AccountID nếu cần
            });
        }


    }
}
