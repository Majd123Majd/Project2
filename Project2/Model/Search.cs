using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project2.Models
{
    public class Search
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public int userId { get; set; }
        public DateTime CreatedDate { get; set; }

        [ForeignKey("userId")]
        public User User {get; set;}
    }
}
