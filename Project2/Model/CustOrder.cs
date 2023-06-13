using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project2.Models
{
        public class CustOrder
    {
        [Key]
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int OrderId { get; set; }
        public DateTime CreatedDate { get; set; }

        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }
        [ForeignKey("OrderId")]
        public Order Order { get; set; }
    }
}