using odaurehonbe.Data;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class BusBusRoute
{
    [Key]
    public int BusBusRouteID { get; set; }  // ID duy nhất cho mối quan hệ

    public int BusID { get; set; }  // Khóa ngoại đến Bus

    public int BusRouteID { get; set; }  // Khóa ngoại đến BusRoute
      [JsonIgnore]
    public Bus Bus { get; set; }  // Mối quan hệ với Bus
    [JsonIgnore]
    public BusRoute BusRoute { get; set; }  // Mối quan hệ với BusRoute
}
