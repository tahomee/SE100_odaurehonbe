using System.ComponentModel.DataAnnotations;

namespace odaurehonbe.Data
{
    public class TicketClerk : Account
    {
       
        public int StaffID { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime HireDate { get; set; }
    }
}
