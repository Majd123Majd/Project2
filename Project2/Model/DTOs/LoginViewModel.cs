using System.ComponentModel.DataAnnotations;

namespace Project2.Model.DTOs
{
    public class LoginViewModel
    {
        [Required]
        public string UserName { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
