using System.ComponentModel.DataAnnotations;

namespace odaurehonbe.Data
{
    public class BusRoute
    {
        [Key]
        public int BusRouteID { get; set; }
        public int Duration { get; set; }
        public int DepartPlace { get; set; }
        public int ArrivalPlace { get; set; }
        public string Type { get; set; }
    }
}
