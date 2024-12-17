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

            if (_dbContext.Accounts.Any(a => a.UserName == customer.Account.UserName))
            {
                return BadRequest(new { message = "Username already exists." });
            }

            var account = new Account
            {
                UserName = customer.Account.UserName,
                Password = customer.Account.Password,
                UserType = customer.Account.UserType,
                Status = "Active" 
            };

            _dbContext.Accounts.Add(account);
            await _dbContext.SaveChangesAsync();

            customer.AccountID = account.AccountID; 
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

            var account = await _dbContext.Accounts
                .FirstOrDefaultAsync(a => a.UserName == request.Email && a.Password == request.Password);

            if (account == null)
            {
                return Unauthorized(new { message = "Invalid email or password." });
            }

            return Ok(new
            {
                message = "Login successful!",
                accountType = account.UserType,
                accountId = account.AccountID 
            });
        }
        [HttpPost("signout")]
        public IActionResult SignOut()
        {
            return Ok(new { message = "Signout successful. Please clear local storage." });
        }
      


    }
}
