using System.ComponentModel.DataAnnotations;

namespace Project2.Services
{
    public class LoginViewModel
    {
        [Required]
        public string UserName { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
