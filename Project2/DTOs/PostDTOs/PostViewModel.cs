using Project2.Model.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project2.DTOs.PostDTOs
{
    public class PostViewModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int marketerId { get; set; }
        public int productId { get; set; }
        public int counter { get; set; }
        public DateTime CreatedDate { get; set; }

        public string Marketer_Name { get; set; }

        public string Product_Name { get; set; }
    }
}
