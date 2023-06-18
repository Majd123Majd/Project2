using Project2.DTOs.NotifyDTOs;
using Project2.Model.Helpers;

namespace Project2.Repository.Interfaces
{
    public interface INotifyServices
    {
        ApiResponse ViewNotificationsList(int userId);
        ApiResponse ViewSearchList(int userId);
        // ApiResponse ViewNotificationDetails();
        ApiResponse AddNotification(int senderId, int receiverId ,AddNotificationViewModel addNotification);
        ApiResponse DeleteNotification(int userId ,int notificationId);
        ApiResponse AddSearch(int userId ,AddSearchViewModel addSearch);
        ApiResponse DeleteSearch(int userId,int searchId);
    }
}
