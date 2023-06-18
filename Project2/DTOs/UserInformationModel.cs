using System.ComponentModel.DataAnnotations.Schema;

namespace Project2.DTOs
{
    public class UserInformationModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string password { get; set; }
        public string ConfirmPassword { get; set; }
        public int phoneNumber { get; set; }
        public int age { get; set; }
        public string city { get; set; }
        public string state { get; set; }


    }
}
