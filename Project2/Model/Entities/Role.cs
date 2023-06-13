using System.ComponentModel.DataAnnotations;

namespace Project2.Model.Entities
{
    public class Role
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
