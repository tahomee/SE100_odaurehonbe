using System.ComponentModel.DataAnnotations;

namespace odaurehonbe.Data
{
    public class Bus
    {
        [Key]
        public int BusID { get; set; }
        public int NumSeat { get; set; }
        public DateTime DepartTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public int DriverID { get; set; }
        public string PlateNum { get; set; }
    }
}
