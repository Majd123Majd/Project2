using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project2.Models
{
    public class Maintainer
    {
    [Key]
    public int Id { get; set; } 
    public string Name { get; set; }
    public string Email { get; set; }

    public int RoleId { get; set; }
    [ForeignKey("RoleId")]
    public Role Role { get; set; }
    }
}
