using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project2.Models
{
    public class DelivOrder
    {
        [Key]
        public int Id { get; set; }
        public int DeliverId { get; set; }
        public int OrderId { get; set; }
        public DateTime CreatedDate { get; set; }

        [ForeignKey("DeliverId")]
        public Deliver Deliver { get; set; }
        [ForeignKey("OrderId")]
        public Order Order { get; set; }
        public ICollection<Order> Orders { get; set; }

    }
}