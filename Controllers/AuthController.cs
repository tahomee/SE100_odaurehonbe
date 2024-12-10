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
                string.IsNullOrEmpty(customer.Email) ||
                string.IsNullOrEmpty(customer.Password) ||
                string.IsNullOrEmpty(customer.PhoneNumber))
            {
                return BadRequest(new { message = "Please fill in all the required fields." });
            }

            if (_dbContext.Accounts.Any(a => a.UserName == customer.Email))
            {
                return BadRequest(new { message = "Email already exists." });
            }

           
           
            _dbContext.Accounts.Add(customer); 

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

            var account = await _dbContext.Accounts
                .FirstOrDefaultAsync(a => a.UserName == request.Email && a.Password == request.Password);

            if (account == null)
            {
                return Unauthorized(new { message = "Invalid email or password." });
            }

            string accountType = account.UserType; 

            return Ok(new { message = "Đăng nhập thành công!", accountType });
        }

    }
}
