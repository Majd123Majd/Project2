using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Project2.Models
{
    public class TenderComm
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        [ForeignKey("Marketer")]
        public int MarketerId { get; set; }
        [ForeignKey("Tender")]
        public int TenderId { get; set; }
        public DateTime CreatedDate { get; set; }

        public Tender Tender { get; set; }
        public Marketer Marketer { get; set; }

    }
}
