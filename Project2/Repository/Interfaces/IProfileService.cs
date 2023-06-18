using Project2.DTOs.CustomerDTOs;
using Project2.DTOs.DeliverDTOs;
using Project2.DTOs.MarketerDTOs;
using Project2.Model.Helpers;

namespace Project2.Repository.Interfaces
{
    public interface IProfileService
    {
        ApiResponse ViewMarketerProfile(int userId);
        ApiResponse ViewCustomerProfile(int userId);
        ApiResponse ViewDeliverProfile(int userId);
        ApiResponse EditCustomerProfile(int userId , UpdateCustomerViewModel updateCustomer );
        ApiResponse EditMarketerProfile(int userId, UpdateMarketerViewModel updateMarketer);
        ApiResponse EditDeliverProfile(int userId, UpdateDeliverViewModel updateDeliver);
        ApiResponse ViewFollowingPagesList(int userId);
        ApiResponse ViewFriendsList(int userId);
        ApiResponse FollowMarketer(int userId , int marketerId );
        ApiResponse UnfollowMarketer(int userId , int marketerId );
        ApiResponse FollowCustomer(int userId, int customerId);
        ApiResponse UnfollowCustomer(int userId, int customerId);
        ApiResponse ContractWithDeliver(int userId , int deliverId);
        ApiResponse CancelContractWithDeliver(int userId, int deliverId);
        ApiResponse AddAgent(int userId, int customerId);
        ApiResponse RemoveAgent(int userId, int customerId);

    }
}
