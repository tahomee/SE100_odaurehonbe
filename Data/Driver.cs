using System.ComponentModel.DataAnnotations;

namespace odaurehonbe.Data
{
    public class Driver : Account
    {
       
        public int DriverID { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string LicenseNumber { get; set; }
    }
}
