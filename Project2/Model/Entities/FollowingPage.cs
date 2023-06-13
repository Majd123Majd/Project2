using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project2.Model.Entities
{

    public class FollowingPage
    {
        [Key]
        public int Id { get; set; }
        public int pageId { get; set; }
        public int userId { get; set; }
        public DateTime CreatedDate { get; set; }

        [ForeignKey("pageId")]
        public Marketer Marketer { get; set; }

        [ForeignKey("userId")]
        public User User { get; set; }
    }

}
