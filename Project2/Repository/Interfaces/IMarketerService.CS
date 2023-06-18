using Project2.Model.Helpers;

namespace Project2.Repository.Interfaces
{
    public interface IMarketerService
    {
        ApiResponse GetAllMarketers(ComplexFilter Filter);
    }
}
