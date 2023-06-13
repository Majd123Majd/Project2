using Project2.Model.DTOs;
using Project2.Model.DTOs.CustomerDTOs;

namespace Project2.Repository.Interfaces
{
    public interface IUserService
    {
        string Login(LoginViewModel Login);
        Task<string> RegisterForCustomer(AddCustomerViewModel NewCustomer);
    }
}
