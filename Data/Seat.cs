using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace odaurehonbe.Data
{
    public class Seat
    {
        [Key]
        public int SeatID { get; set; }
        public string SeatNumber { get; set; }
        public bool IsBooked { get; set; } 

        public int BusBusRouteID { get; set; } 
        [JsonIgnore]
        public BusBusRoute BusBusRoute { get; set; } 

    }
}
