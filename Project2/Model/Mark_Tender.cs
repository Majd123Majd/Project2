using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Project2.Models
{
    public class Mark_Tender
    {
        [Key]
        public int Id { get; set; }
        public int MarketerId { get; set; }
        public int TenderId { get; set; }
        public DateTime CreatedDate { get; set; }

        [ForeignKey("TenderId")]
        public Tender Tender {get; set;}
        [ForeignKey("MarketerId")]
        public Marketer Marketer {get; set;}
    }
}
