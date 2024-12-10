using System.ComponentModel.DataAnnotations;

namespace odaurehonbe.Data
{
    public class Payment
    {
        [Key]
        public int PaymentID { get; set; }
        public int TicketID { get; set; }
        public int CustomerID { get; set; }
        public DateTime PaymentTime { get; set; }
        public int StaffID { get; set; }
        public int PromoID { get; set; }
        public decimal TotalFee { get; set; }
        public string PaymentMethod { get; set; }
    }
}
