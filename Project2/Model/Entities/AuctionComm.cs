using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project2.Model.Entities
{
    public class AuctionComm
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public int CustomerId { get; set; }
        public int AuctionId { get; set; }
        public DateTime CreatedDate { get; set; }

        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }
        [ForeignKey("AuctionId")]
        public Auction Auction { get; set; }
    }
}
