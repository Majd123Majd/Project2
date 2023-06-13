using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project2.Model.Entities
{
    public class Friend
    {
        [Key]
        public int Id { get; set; }
        public int friendId { get; set; }
        public int userId { get; set; }
        public DateTime CreatedDate { get; set; }

        [ForeignKey("friendId")]
        public Customer Customer { get; set; }

        [ForeignKey("userId")]
        public User User { get; set; }
    }
}
