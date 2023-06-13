using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Project2.Models
{
    public class Post
    {
        [Key]
        public int Id { get; set; }
        public string Description { get; set; }
        public int marketerId { get; set; }
        public int productId { get; set; }
        public int counter { get; set;}
        public DateTime CreatedDate { get; set; }

        [ForeignKey("marketerId")]
        public Marketer Marketer { get; set; }

        [ForeignKey("productId")]
        public Product Product { get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<CustPost> CustPosts { get; set; }
        public ICollection<Interaction> Interactions { get; set; }

    }
}
