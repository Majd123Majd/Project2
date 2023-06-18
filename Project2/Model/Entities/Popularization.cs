using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
//using System.Data.Entity;

namespace Project2.Model.Entities
{
    public class Popularization
    {
        [Key]
        public int Id { get; set; }
        public string Discretion { get; set; }
        [ForeignKey("Marketer")]
        public int marketerId { get; set; }
        [ForeignKey("Product")]
        public int productId { get; set; }
        public string zone { get; set; }
        public int fromAge { get; set; }
        public int toAge { get; set; }
        public DateTime CreatedDate { get; set; }
        public Marketer Marketer { get; set; }
        public Product Product { get; set; }
    }
}
