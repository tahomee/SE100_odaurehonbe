using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using odaurehonbe.Data;
using odaurehonbe.Models;

namespace odaurehonbe.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ILogger<AccountController> _logger;
        public AccountController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAccounts()
        {
            var accounts = await _context.Accounts
                .Include(a => a.Customer)
                .Include(a => a.Driver)
                .Include(a => a.TicketClerk)
                .ToListAsync();

            var result = accounts.Select(account =>
            {
                switch (account.UserType)
                {
                    case "Driver":
                        var driver = account.Driver;
                        return driver != null ? new AccountDto
                        {
                            AccountID = account.AccountID,
                            UserName = account.UserName,
                            Status = account.Status,
                            UserType = account.UserType,
                            Password = account.Password,
                            Name = driver.Name,
                            Gender = driver.Gender,
                            PhoneNumber = driver.PhoneNumber,
                            LicenseNumber = driver.LicenseNumber
                        } : null;

                    case "TicketClerk":
                        var clerk = account.TicketClerk;
                        return clerk != null ? new AccountDto
                        {
                            AccountID = account.AccountID,
                            UserName = account.UserName,
                            Status = account.Status,
                            Name = clerk.Name,
                            UserType = account.UserType,
                            Password = account.Password,
                            Gender = clerk.Gender,
                            PhoneNumber = clerk.PhoneNumber,
                            HireDate = clerk.HireDate
                        } : null;

                    case "Customer":
                        var customer = account.Customer;
                        return customer != null ? new AccountDto
                        {
                            AccountID = account.AccountID,
                            UserName = account.UserName,
                            Status = account.Status,
                            Name = customer.Name,
                            UserType = account.UserType,
                            Password = account.Password,
                            Gender = customer.Gender,
                            PhoneNumber = customer.PhoneNumber,
                            Address = customer.Address
                        } : null;

                    default:
                        return null;
                }
            }).Where(a => a != null).ToList();

            return Ok(result);
        }

        [HttpGet("{keyword}")]
        public async Task<IActionResult> SearchAccounts(string? keyword)
        {
            // Lấy danh sách tài khoản dựa trên từ khóa tìm kiếm
            var accounts = await _context.Accounts
                .Include(a => a.Customer)
                .Include(a => a.Driver)
                .Include(a => a.TicketClerk)
               .Where(a => string.IsNullOrEmpty(keyword) ||
            a.UserName.Contains(keyword) ||
            a.AccountID.ToString().Contains(keyword) ||
            (a.Customer != null && a.Customer.Name.Contains(keyword)) ||
            (a.Driver != null && a.Driver.Name.Contains(keyword)) ||
            (a.TicketClerk != null && a.TicketClerk.Name.Contains(keyword)))
                .ToListAsync();

            // Chuyển danh sách Account sang AccountDto
            var accountDtos = accounts.Select(account => account.UserType switch
            {
                "Driver" when account.Driver != null => new AccountDto
                {
                    AccountID = account.AccountID,
                    UserName = account.UserName,
                    Status = account.Status,
                    UserType = account.UserType,
                    Password = account.Password,
                    Name = account.Driver.Name,
                    Gender = account.Driver.Gender,
                    PhoneNumber = account.Driver.PhoneNumber,
                    LicenseNumber = account.Driver.LicenseNumber
                },
                "TicketClerk" when account.TicketClerk != null => new AccountDto
                {
                    AccountID = account.AccountID,
                    UserName = account.UserName,
                    Status = account.Status,
                    UserType = account.UserType,
                    Password = account.Password,
                    Name = account.TicketClerk.Name,
                    Gender = account.TicketClerk.Gender,
                    PhoneNumber = account.TicketClerk.PhoneNumber,
                    HireDate = account.TicketClerk.HireDate
                },
                "Customer" when account.Customer != null => new AccountDto
                {
                    AccountID = account.AccountID,
                    UserName = account.UserName,
                    Status = account.Status,
                    Name = account.Customer.Name,
                    Gender = account.Customer.Gender,
                    UserType = account.UserType,
                    Password = account.Password,
                    PhoneNumber = account.Customer.PhoneNumber,
                    Address = account.Customer.Address
                },
                _ => null
            }).Where(dto => dto != null).ToList(); // Loại bỏ các DTO null

            // Trả về danh sách AccountDto
            return Ok(accountDtos);
        }



        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAccount(int id, [FromBody] AccountDto accountDto)
        {
            if (id != accountDto.AccountID)
            {
                return BadRequest("Account ID mismatch");
            }

            var account = await _context.Accounts
                .Include(a => a.Customer)
                .Include(a => a.Driver)
                .Include(a => a.TicketClerk)
                .FirstOrDefaultAsync(a => a.AccountID == id);

            if (account == null)
            {
                return NotFound("Account not found");
            }

            // Update common account fields
            account.UserName = accountDto.UserName;
            account.Status = accountDto.Status;
            account.Password = accountDto.Password; // Ensure this is hashed in production

            // Update specific user type fields
            switch (accountDto.UserType)
            {
                case "Customer":
                    if (account.Customer == null)
                    {
                        account.Customer = new Customer();
                    }
                    account.Customer.Name = accountDto.Name;
                    account.Customer.Gender = accountDto.Gender;
                    account.Customer.PhoneNumber = accountDto.PhoneNumber;
                    account.Customer.Address = accountDto.Address;
                    break;

                case "Driver":
                    if (account.Driver == null)
                    {
                        account.Driver = new Driver();
                    }
                    account.Driver.Name = accountDto.Name;
                    account.Driver.Gender = accountDto.Gender;
                    account.Driver.PhoneNumber = accountDto.PhoneNumber;
                    account.Driver.LicenseNumber = accountDto.LicenseNumber;
                    break;

                case "TicketClerk":
                    if (account.TicketClerk == null)
                    {
                        account.TicketClerk = new TicketClerk();
                    }
                    account.TicketClerk.Name = accountDto.Name;
                    account.TicketClerk.Gender = accountDto.Gender;
                    account.TicketClerk.PhoneNumber = accountDto.PhoneNumber;
                    account.TicketClerk.HireDate = accountDto.HireDate.Value;
                    break;

                default:
                    return BadRequest("Invalid user type");
            }

            try
            {
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Error updating account with ID {Id}", id);
                return StatusCode(500, "An error occurred while updating the account");
            }
        }


        [HttpPost]
        public async Task<ActionResult<Account>> PostAccount(AccountDto accountDto)
        {
            // Validate the incoming accountDto to check if UserType and Password are provided
            if (string.IsNullOrEmpty(accountDto.UserType))
            {
                return BadRequest("UserType is required.");
            }

            if (string.IsNullOrEmpty(accountDto.Password))
            {
                return BadRequest("Password is required.");
            }

            // Validate if the 'Name' field is provided for the specific user type
            if (string.IsNullOrEmpty(accountDto.Name) && accountDto.UserType != "TicketClerk")
            {
                return BadRequest("Name is required for the selected user type.");
            }

            // Create a new Account based on the provided DTO
            var account = new Account
            {
                AccountID = accountDto.AccountID,
                UserName = accountDto.UserName,
                Status = accountDto.Status,
                UserType = accountDto.UserType,
                Password = accountDto.Password // Store the password as plain text
            };

            // If AccountID is provided from frontend, set it
            if (accountDto.AccountID > 0)  // You can apply additional validation if necessary
            {
                account.AccountID = accountDto.AccountID;
            }

            // Add the corresponding user type (Customer, Driver, or TicketClerk) based on UserType
            if (accountDto.UserType == "Driver")
            {
                var driver = new Driver
                {
                    AccountID = accountDto.AccountID,
                    Name = accountDto.Name,
                    Gender = accountDto.Gender,
                    PhoneNumber = accountDto.PhoneNumber,
                    LicenseNumber = accountDto.LicenseNumber
                };
                account.Driver = driver;
            }
            else if (accountDto.UserType == "TicketClerk")
            {
                var ticketClerk = new TicketClerk
                {
                    AccountID = accountDto.AccountID,
                    Name = accountDto.Name,
                    Gender = accountDto.Gender,
                    PhoneNumber = accountDto.PhoneNumber,
                    HireDate = accountDto.HireDate.Value // Assuming HireDate is required for TicketClerk
                };
                account.TicketClerk = ticketClerk;
            }
            else if (accountDto.UserType == "Customer")
            {
                var customer = new Customer
                {
                    AccountID = accountDto.AccountID,
                    Name = accountDto.Name,  // Ensure Name is being set correctly
                    Gender = accountDto.Gender,
                    PhoneNumber = accountDto.PhoneNumber,
                    Address = accountDto.Address
                };
                account.Customer = customer;
            }
            else
            {
                return BadRequest("Invalid UserType.");
            }

            // Add the new Account (and related User) to the database
            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAccount", new { id = account.AccountID }, account);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccount(int id)
        {
            var account = await _context.Accounts
                .Include(a => a.Customer)
                .Include(a => a.Driver)
                .Include(a => a.TicketClerk)
                .FirstOrDefaultAsync(a => a.AccountID == id);

            if (account == null)
            {
                return NotFound("Account not found");
            }

            // Xóa các liên kết liên quan trước khi xóa account
            if (account.Customer != null)
            {
                _context.Customers.Remove(account.Customer);
            }
            if (account.Driver != null)
            {
                _context.Drivers.Remove(account.Driver);
            }
            if (account.TicketClerk != null)
            {
                _context.TicketClerks.Remove(account.TicketClerk);
            }

            _context.Accounts.Remove(account);

            try
            {
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, "An error occurred while deleting the account");
            }
        }


        private bool AccountExists(int id)
        {
            return _context.Accounts.Any(e => e.AccountID == id);
        }
    }
}
