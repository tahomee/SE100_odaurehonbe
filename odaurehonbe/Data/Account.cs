using System.ComponentModel.DataAnnotations;

namespace odaurehonbe.Data
{
    public class Account
    {
        [Key]
        public int AccountID { get; set; }
        public string UserType { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Status { get; set; }
    }
}
