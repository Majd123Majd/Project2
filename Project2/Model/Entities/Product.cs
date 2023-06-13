using System.ComponentModel.DataAnnotations;

namespace Project2.Model.Entities
{
    public class Product
    {
        [Key]
        public int id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
        public DateTime CreatedDate { get; set; }

        public ICollection<Post> Posts { get; set; }
    }
}
