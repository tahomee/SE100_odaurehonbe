using System.ComponentModel.DataAnnotations;

namespace odaurehonbe.Data
{
    public class Ticket
    {
        [Key]
        public int TicketID { get; set; }
        public int BusBusRouteID { get; set; }
        public int CustomerID { get; set; }
        public int SeatNum { get; set; }
        public DateTime BookingDate { get; set; }
        public string? Type { get; set; }
        public decimal Price { get; set; }
        public string? Status { get; set; }
        public int? PaymentID { get; set; }
    }
}
