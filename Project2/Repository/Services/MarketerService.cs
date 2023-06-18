using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Project2.DTOs.MarketerDTOs;
using Project2.Model;
using Project2.Model.Helpers;
using Project2.Repository.Interfaces;
using System.Reflection;

namespace Project2.Repository.Services
{
    public class MarketerService : IMarketerService
    {
        public AppDbContext _DbContext { get; set; }
        private IMapper _Mapper;

        public MarketerService(AppDbContext DbContext , IMapper Mapper)
        {
            _DbContext = DbContext;
            _Mapper = Mapper;
        }
        public ApiResponse GetAllMarketers(ComplexFilter Filter)
        {
            List<MarketerViewModel> Marketers = _Mapper.Map<List<MarketerViewModel>>(_DbContext.Marketers
                .Include(x => x.User)
                .Where(x => (!string.IsNullOrEmpty(Filter.SearchQuery) ?
                    x.User.Name.ToLower().StartsWith(Filter.SearchQuery) : true)).ToList());

            int Count = Marketers.Count();

            if (!string.IsNullOrEmpty(Filter.Sort))
            {
                PropertyInfo? SortProperty = typeof(MarketerViewModel).GetProperty(Filter.Sort);

                if (SortProperty != null && Filter.Order == "asc")
                    Marketers = Marketers.OrderBy(x => SortProperty.GetValue(x)).ToList();

                else if (SortProperty != null && Filter.Order == "desc")
                    Marketers = Marketers.OrderByDescending(x => SortProperty.GetValue(x)).ToList();

                Marketers = Marketers.Skip((Filter.PageIndex - 1) * Filter.PageSize)
                    .Take(Filter.PageSize).ToList();
            }
            else
                Marketers = Marketers.Skip((Filter.PageIndex - 1) * Filter.PageSize)
                    .Take(Filter.PageSize).ToList();

            return new ApiResponse(Marketers, "Succeed", Count);

        }
    }
}
