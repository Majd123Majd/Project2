using AutoMapper;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.EntityFrameworkCore;
using Project2.DTOs.PostDTOs;
using Project2.DTOs.ProductDTOs;
using Project2.Model;
using Project2.Model.Helpers;
using Project2.Repository.Interfaces;
using System.Diagnostics.Metrics;
using System.Reflection;

namespace Project2.Repository.Services
{
    public class PostService : IPostService
    {
        public AppDbContext _DbContext { get; set; }
        private IMapper _Mapper;

        public PostService(AppDbContext DbContext, IMapper Mapper)
        {
            _DbContext = DbContext;
            _Mapper = Mapper;
        }
        public ApiResponse GetAllPosts(ComplexFilter Filter)
        {
            List<PostViewModel> Posts = _Mapper.Map<List<PostViewModel>>(_DbContext.Posts
                .Include(x => x.Product)
                .Where(x => (!string.IsNullOrEmpty(Filter.SearchQuery) ?
                    x.Product.Name.ToLower().StartsWith(Filter.SearchQuery) : true)).ToList());
            
            int Count = Posts.Count();
            
            PropertyInfo? SortByCounter = typeof(PostViewModel).GetProperty("counter");
            Posts = Posts.OrderBy(x => SortByCounter.GetValue(x)).ToList();

            if (!string.IsNullOrEmpty(Filter.Sort))
            {
                PropertyInfo? SortProperty = typeof(PostViewModel).GetProperty(Filter.Sort);

                if (SortProperty != null && Filter.Order == "asc")
                    Posts = Posts.OrderBy(x => SortProperty.GetValue(x)).ToList();

                else if (SortProperty != null && Filter.Order == "desc")
                    Posts = Posts.OrderByDescending(x => SortProperty.GetValue(x)).ToList();

                Posts = Posts.Skip((Filter.PageIndex - 1) * Filter.PageSize)
                    .Take(Filter.PageSize).ToList();
            }
            else
                Posts = Posts.Skip((Filter.PageIndex - 1) * Filter.PageSize)
                    .Take(Filter.PageSize).ToList();

            return new ApiResponse(Posts, "Succeed", Count);
        }
    }
}
