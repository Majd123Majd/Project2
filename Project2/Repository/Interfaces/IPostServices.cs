using Project2.Model.Helpers;
namespace Project2.Repository.Interfaces
{
    public interface IPostServices
    {
        ApiResponse ViewPostForMarketer(int userId);
        ApiResponse ViewPostForCustomer(int userId);
        ApiResponse ShowPostDetails(int postId);
        Task<ApiResponse> AddPost();
        ApiResponse DeletePost(int postId);
        ApiResponse UpdatePost();
        ApiResponse InteractionOnPost(int userId , int postId);
    }
}
