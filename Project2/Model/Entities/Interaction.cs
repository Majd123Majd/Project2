using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project2.Model.Entities
{
    public class Interaction
    {
        [Key]
        public int Id { get; set; }
        public string ReactionType { get; set; }
        public int PostId { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        [ForeignKey("PostId")]
        public Post Post { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
