using Project2.Data.Enum;

namespace Project2.DTOs.ProductDTOs
{
    public class AddProductViewModel
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public string photo { get; set; }
        public ProductType? productType { get; set; }
    }
}
