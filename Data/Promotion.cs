using System.ComponentModel.DataAnnotations;

namespace odaurehonbe.Data
{
    public class Promotion
    {
        [Key]
        public int PromoID { get; set; }
        public string Name { get; set; }
        public float DiscountPercent { get; set; }
        public decimal Discount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ICollection<Promotion>? Promions { get; set; }
    }
}
