using System.ComponentModel.DataAnnotations;

namespace odaurehonbe.Data
{
    public class BusStop
    {
        [Key]
        public int BusStopID { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
    }
}
