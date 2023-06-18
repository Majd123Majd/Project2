using AutoMapper;
using Project2.DTOs.CustomerDTOs;
using Project2.DTOs.DeliverDTOs;
using Project2.DTOs.MarketerDTOs;
using Project2.DTOs.UserDTOs;
using Project2.Model.Entities;

namespace Project2.Model.AutoMapperProfile
{
    public class MyAutoMapper : Profile
    {
        public MyAutoMapper()
        {
            // Customer..
            CreateMap<Customer, AddCustomerViewModel>().ReverseMap();
            
            // User..
            CreateMap<User, AddUserViewModel>().ReverseMap();

            // Deliver..
            CreateMap<Deliver, AddDeliverViewModel>().ReverseMap();

            // Marketer..
            CreateMap<Marketer, AddMarketerViewModel>().ReverseMap();
            CreateMap<Marketer, MarketerViewModel>()
                .ForMember(c => c.user_name, c => c.MapFrom(f => f.User.Name));
        }
    }
}
