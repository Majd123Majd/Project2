using Project2.Data.Enum;
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
        [ForeignKey("ComplainedOn")]
        public int ComplainedOnId { get; set; }
        public ComplaintType complaintType { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedDate { get; set; }

        public User Complainer { get; set; }
        public User ComplainedOn { get; set; }

    }
}
