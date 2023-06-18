using System.ComponentModel.DataAnnotations;

namespace Project2.DTOs.CustomerDTOs
{
    public class UpdateCustomerViewModel
    {
        public string? Name { get; set; }
        public int phoneNumber { get; set; }
        public string? photo { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Birthdate")]
        [DataType(DataType.Date)]
        public DateTime Birthdate { get; set; }
        public string? city { get; set; }
        public string? zone { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
