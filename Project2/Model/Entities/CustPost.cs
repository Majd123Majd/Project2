using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project2.Model.Entities
{
    public class CustPost
    {
        [Key]
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int PostId { get; set; }
        public DateTime CreatedDate { get; set; }
        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }
        [ForeignKey("PostId")]
        public Post Post { get; set; }

    }
}
