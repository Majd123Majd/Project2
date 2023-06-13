using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Project2.Model.Entities
{
    public class Deliver
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int phoneNumber { get; set; }
        public int AddressId { get; set; }
        public string? photo { get; set; }
        public string? AccountCach { get; set; }
        public int userId { get; set; }
        public DateTime CreatedDate { get; set; }


        [ForeignKey("userId")]
        public User User { get; set; }
        [ForeignKey("AddressId")]
        public Address Address { get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<DelivOrder> DelivOrders { get; set; }
    }
}
