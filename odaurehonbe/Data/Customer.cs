namespace PostgreSQL.Data
{
    public class Customer
    {
        public int CustomerID { get; set; }
        public string Name { get; set; }
        public char? Gender { get; set; } 
        public string PhoneNumber { get; set; }
        public string Email { get; set; } 
        public string Address { get; set; } 
    }
}
