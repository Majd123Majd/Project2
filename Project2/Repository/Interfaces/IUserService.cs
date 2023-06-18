using Project2.DTOs;
using Project2.DTOs.CustomerDTOs;
using Project2.DTOs.DeliverDTOs;
using Project2.DTOs.MarketerDTOs;
using Project2.Model.Helpers;

namespace Project2.Repository.Interfaces
{
    public interface IUserService
    {
        ApiResponse Login(LoginViewModel Login);
        Task<ApiResponse> RegisterForCustomer(AddCustomerViewModel NewCustomer);
        Task<ApiResponse> RegisterForDeliver(AddDeliverViewModel NewDeliver);
        Task<ApiResponse> RegisterForMarketer(AddMarketerViewModel NewMarketer);
    }
}
