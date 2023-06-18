using Project2.Data.Enum;
using System.ComponentModel.DataAnnotations;

namespace Project2.Model.Entities
{
    public class Product
    {
        [Key]
        public int id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string photo { get; set; }
        public DateTime CreatedDate { get; set; }
        public ProductType? productType { get; set; }
        public ICollection<Post> Posts { get; set; }
    }
}
