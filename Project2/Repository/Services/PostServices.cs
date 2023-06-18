using Microsoft.EntityFrameworkCore;
using Project2.Model;
using Project2.Model.Helpers;
using Project2.Repository.Interfaces;

namespace Project2.Repository.Services
{
    public class PostServices : IPostServices
    {
        public AppDbContext _dbContext { get; set; }
        public PostServices(AppDbContext DbContext)
        {
            _dbContext = DbContext;
        }
        public Task<ApiResponse> AddPost()
        {
            throw new NotImplementedException();
        }

        public ApiResponse DeletePost(int postId)
        {
            var post = _dbContext.Posts.FirstOrDefault(p => p.Id == postId);
            if (post != null)
                return new ApiResponse(post, "");

            _dbContext.Posts.Remove(post);
            _dbContext.SaveChanges();

            return new ApiResponse(post , "");
        }

        public ApiResponse InteractionOnPost(int userId, int postId)
        {
           // var interaction = _dbContext.Interactions
            throw new NotImplementedException();
        }

        public ApiResponse ShowPostDetails(int postId )
        {
            var post = _dbContext.Posts
                .FirstOrDefault(p => p.Id == postId);
            if (post == null)
                return new ApiResponse();
            var product = _dbContext.Products.FirstOrDefault(pr => pr.id == post.productId);
            var marketer = _dbContext.Marketers.FirstOrDefault(m => m.Id == post.marketerId);

            throw new NotImplementedException();
        }

        public ApiResponse UpdatePost()
        {
            throw new NotImplementedException();
        }

        public ApiResponse ViewPostForCustomer(int userId)
        {
            var customer = _dbContext.Customers.FirstOrDefault(c => c.userId == userId);
            if (customer == null)
                return new ApiResponse(customer , "user not found");

            var customerpost = _dbContext.CustPosts
                .FirstOrDefault(c => c.CustomerId == customer.Id);
            if (customerpost == null)
                return new ApiResponse(customerpost ,"customer post not found");
            
            return new ApiResponse(customerpost, "Seccess");
        }

        public ApiResponse ViewPostForMarketer(int userId)
        {

            var marketer = _dbContext.Marketers.FirstOrDefault(c => c.userId == userId);
            if (marketer == null)
                return new ApiResponse(marketer, "user not found");

            var marketerpost = _dbContext.Posts
                .FirstOrDefault(c => c.marketerId == marketer.Id);
            if (marketerpost == null)
                return new ApiResponse(marketerpost, "customer post not found");

            return new ApiResponse(marketerpost, "Seccess");
        }
    }
}
