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

        public int BusID { get; set; } 
        [JsonIgnore] 
        public Bus Bus { get; set; } 
    }
}
