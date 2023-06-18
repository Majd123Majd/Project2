using Project2.Model.Helpers;

namespace Project2.Repository.Interfaces
{
    public interface IProductService
    {
        ApiResponse GetAllProducts(ComplexFilter Filter);
    }
}
