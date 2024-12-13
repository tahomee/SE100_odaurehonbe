using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace odaurehonbe.Data
{
    public class BusRoute
    {
        [Key]
        public int BusRouteID { get; set; }

        public string DepartPlace { get; set; }
         public string ArrivalPlace { get; set; }


        public DateTime DepartureTime { get; set; }

        public string Duration { get; set; }
        [JsonIgnore]
        public ICollection<BusBusRoute> BusBusRoutes { get; set; }
    }

}

