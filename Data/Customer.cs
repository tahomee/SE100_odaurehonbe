using System.ComponentModel.DataAnnotations;

namespace odaurehonbe.Data
{
    public class Customer : Account
    {
        public int CustomerID { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
    }
}
