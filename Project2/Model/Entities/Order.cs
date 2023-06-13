using Project2.Data.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project2.Model.Entities
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public int postId { get; set; }
        public int customerId { get; set; }
        public int deliverId { get; set; }
        public PayWay payWay { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedDate { get; set; }

        [ForeignKey("postId")]
        public Post Post { get; set; }
        [ForeignKey("customerId")]
        public Customer Customer { get; set; }
        [ForeignKey("deliverId")]
        public Deliver Deliver { get; set; }

        public ICollection<CustOrder> CustOrders { get; set; }
        public ICollection<DelivOrder> DelivOrders { get; set; }

    }
}
