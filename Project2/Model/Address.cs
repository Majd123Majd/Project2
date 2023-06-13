using System.ComponentModel.DataAnnotations;

namespace Project2.Models
{
    public class Address
    {
        [Key]
        public int Id { get; set; }
        public string city { get; set; }
        public string state { get; set; }
    }
}
