using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project2.Model.Entities
{
    public class Complaint
    {
        [Key]
        public int Id { get; set; }
        public string? Discretion { get; set; }
        [ForeignKey("Complainer")]
        public int ComplainerId { get; set; }
        public string URL { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public User Complainer { get; set; }

    }
}
