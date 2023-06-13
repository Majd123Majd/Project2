using Project2.Data.Enum;

namespace Project2.Services
{
    public class RegisterViewModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string password { get; set; }
        public int age { get; set; }
        public string state { get; set; }
        public string city { get; set; }
        public int phonNumber { get; set; }
        public UserType? userType { get; set; }
    }
}
