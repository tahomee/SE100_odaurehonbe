using System.ComponentModel.DataAnnotations;

namespace odaurehonbe.Data
{
    public class BusRoute
    {
        [Key]
        public int BusRouteID { get; set; }

        public string DepartPlace { get; set; }
         public string ArrivalPlace { get; set; }


        public DateTime DepartureTime { get; set; }

        public int Duration { get; set; }
    }

}

