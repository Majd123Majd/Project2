using Project2.Data.Enum;
using System.ComponentModel.DataAnnotations;

namespace Project2.DTOs
{
    public class LoginViewModel
    {
        [Required]
        public string UserName { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
