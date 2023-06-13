using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Project2.Models
{
    public class Tender
    {
        [Key]
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Discretion { get; set; }
        public int CustomerId { get; set; }
        public int? Value { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime FinalDate { get; set; }
        [ForeignKey("CustomerId")]
        public Customer Customer {get; set;}
        public ICollection<TenderComm> TenderComments { get; set; }
        public ICollection<Mark_Tender> Mark_Tenders { get; set; }
    }
}
