using odaurehonbe.Data;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;  // Đừng quên thêm thư viện JsonIgnore

public class Customer
{
    [Key]
    public int AccountID { get; set; }  // Liên kết với Account
    public string Name { get; set; }
    public string Gender { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }

    [JsonIgnore]  
    public Account Account { get; set; }  // Liên kết ngược với Account
}
