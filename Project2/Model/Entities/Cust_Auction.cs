using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project2.Model.Entities
{
    public class Cust_Auction
    {
        [Key]
        public int Id { get; set; }
        public int customerId { get; set; }
        public int AuctionId { get; set; }
        public DateTime CreatedDate { get; set; }

        [ForeignKey("AuctionId")]
        public Auction Auction { get; set; }
        [ForeignKey("customerId")]
        public Customer Customer { get; set; }
    }
}
