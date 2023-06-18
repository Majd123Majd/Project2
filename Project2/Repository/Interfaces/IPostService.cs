using Project2.Model.Helpers;

namespace Project2.Repository.Interfaces
{
    public interface IPostService
    {
        ApiResponse GetAllPosts(ComplexFilter Filter);
    }
}
