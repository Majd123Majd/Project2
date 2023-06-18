using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project2.Model.Entities
{
    public class Notification
    {
        [Key]
        public int Id { get; set; }
        public string Body { get; set; }
        public int SenderId { get; set; }
        public int RecieverId { get; set; }
        public DateTime CreatedDate { get; set; }

        [ForeignKey("SenderId")]
        public User Sender { get; set; }
        [ForeignKey("RecieverId")]
        public User Reciever { get; set; }
    }
}
