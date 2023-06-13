using AutoMapper;
using Project2.Model.DTOs.CustomerDTOs;
using Project2.Model.DTOs.DeliverDTOs;
using Project2.Model.DTOs.UserDTOs;
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
        }
    }
}
