using Microsoft.AspNetCore.Identity;
using Project2.Data.Enum;
using System.ComponentModel.DataAnnotations;

namespace Project2.Models
{
    public class User
    {
        [Key]
        public int UID { get; set; }
        public string Name { get; set; }    
        public string Email { get; set; }
        public string password { get; set; }
        public UserType? userType { get; set; }
        public Customer Customer { get; set; }
        public Marketer Marketer { get; set; }
        public Deliver Deliver { get; set; }
        public ICollection<Interaction> Interactions { get; set; }
        public ICollection<Search> Searches { get; set; }
        // أضف هنا علاقة الشكاوى المبلّغ عنها
        public ICollection<Complaint>? Complaints { get; set; }
        // أضف هنا علاقة الشكاوى المُبلغ عنها
        public ICollection<Complaint>? ComplaintsAgainst { get; set; }

        public ICollection<FollowingPage>? FollowingPages { get; set; }
        public ICollection<Friend>? Friends { get; set; }


    }
}
