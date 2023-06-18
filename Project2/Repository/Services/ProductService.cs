using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Project2.DTOs.MarketerDTOs;
using Project2.DTOs.ProductDTOs;
using Project2.Model;
using Project2.Model.Helpers;
using Project2.Repository.Interfaces;
using System.Reflection;

namespace Project2.Repository.Services
{
    public class ProductService : IProductService
    {
        public AppDbContext _DbContext { get; set; }
        private IMapper _Mapper;

        public ProductService(AppDbContext DbContext, IMapper Mapper)
        {
            _DbContext = DbContext;
            _Mapper = Mapper;
        }
        public ApiResponse GetAllProducts(ComplexFilter Filter)
        {
            List<ProductViewModel> Products = _Mapper.Map<List<ProductViewModel>>(_DbContext.Products
                .Where(x => (!string.IsNullOrEmpty(Filter.SearchQuery) ?
                    x.Name.ToLower().StartsWith(Filter.SearchQuery) : true)).ToList());

            int Count = Products.Count();

            if (!string.IsNullOrEmpty(Filter.Sort))
            {
                PropertyInfo? SortProperty = typeof(ProductViewModel).GetProperty(Filter.Sort);

                if (SortProperty != null && Filter.Order == "asc")
                    Products = Products.OrderBy(x => SortProperty.GetValue(x)).ToList();

                else if (SortProperty != null && Filter.Order == "desc")
                    Products = Products.OrderByDescending(x => SortProperty.GetValue(x)).ToList();

                Products = Products.Skip((Filter.PageIndex - 1) * Filter.PageSize)
                    .Take(Filter.PageSize).ToList();
            }
            else
                Products = Products.Skip((Filter.PageIndex - 1) * Filter.PageSize)
                    .Take(Filter.PageSize).ToList();

            return new ApiResponse(Products, "Succeed", Count);
        }
    }
}
