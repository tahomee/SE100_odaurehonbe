//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Configuration;
//using System.Net.Sockets;
//using System.Security.Principal;

//namespace odaurehonbe.Data
//{
//    public class AppDbContext : DbContext
//    {
//        protected readonly IConfiguration Configuration;

//        public AppDbContext(IConfiguration configuration)
//        {
//            Configuration = configuration;
//        }

//        protected override void OnConfiguring(DbContextOptionsBuilder options)
//        {
//            // connect to postgres with connection string from app settings
//            options.UseNpgsql(Configuration.GetConnectionString("WebApiDatabase"))
//       .EnableSensitiveDataLogging() // Kích hoạt logging chi tiết
//       .LogTo(Console.WriteLine, LogLevel.Information); // Log thông tin

//        }
//        protected override void OnModelCreating(ModelBuilder modelBuilder)
//        {
//            modelBuilder.Entity<Param>().HasNoKey();
//            modelBuilder.Entity<Account>()
//                .HasDiscriminator<string>("UserType")
//                .HasValue<Account>("Account")
//                .HasValue<Customer>("Customer")
//                .HasValue<Driver>("Driver")
//                .HasValue<TicketClerk>("TicketClerk");
//        }
//        public DbSet<Customer> Customers { get; set; }
//        public DbSet<Account> Accounts { get; set; }
//        public DbSet<Bus> Buses { get; set; }
//        public DbSet<BusRoute> BusRoutes { get; set; }
//        public DbSet<BusStop> BusStops { get; set; }
//        public DbSet<Driver> Drivers { get; set; }
//        public DbSet<Ticket> Tickets { get; set; }
//        public DbSet<TicketClerk> TicketClerks { get; set; }
//        public DbSet<Payment> Payments { get; set; }
//        public DbSet<Promotion> Promotions { get; set; }
//        public DbSet<Param> Params { get; set; }

//    }
//}
using Microsoft.EntityFrameworkCore;

namespace odaurehonbe.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Param>().HasNoKey();
            modelBuilder.Entity<Account>()
                .HasDiscriminator<string>("UserType")
                .HasValue<Account>("Account")
                .HasValue<Customer>("Customer")
                .HasValue<Driver>("Driver")
                .HasValue<TicketClerk>("TicketClerk");
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Bus> Buses { get; set; }
        public DbSet<BusRoute> BusRoutes { get; set; }
        public DbSet<BusStop> BusStops { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TicketClerk> TicketClerks { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Promotion> Promotions { get; set; }
        public DbSet<Param> Params { get; set; }
    }
}
