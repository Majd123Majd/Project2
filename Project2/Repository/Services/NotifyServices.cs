using Microsoft.EntityFrameworkCore;
using Project2.DTOs.NotifyDTOs;
using Project2.Model;
using Project2.Model.Entities;
using Project2.Model.Helpers;
using Project2.Repository.Interfaces;

namespace Project2.Repository.Services
{
    public class NotifyServices : INotifyServices
    {
        public AppDbContext _dbContext { get; set; }
        public NotifyServices(AppDbContext DbContext)
        {
            _dbContext = DbContext;
        }
        public ApiResponse AddNotification(int senderId,int receiverId,AddNotificationViewModel addNotification)
        {
            ////_dbContext.Notifications.Add(notification);
            ////await _dbContext.SaveChangesAsync();
            var recuser = _dbContext.Users
                .FirstOrDefault(u => u.UID == receiverId);// :)
            var senduser = _dbContext.Users
                .FirstOrDefault(u => u.UID == senderId); // :}
            if (recuser == null)
                return new ApiResponse(recuser , "ReceiverUsernotFound");
            if (senduser == null)
                return new ApiResponse(senduser ,"SenderUserNotFound");

            var notification = new Notification 
            {
                Body = addNotification.Body,
                SenderId = senduser.UID,
                RecieverId = recuser.UID
            
            };
            _dbContext.Notifications.Add(notification);
            _dbContext.SaveChanges();
                //return await new ApiResponse();
            return new ApiResponse(notification , "Seccess");
        }

        public ApiResponse AddSearch(int userId,AddSearchViewModel addSearch)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.UID == userId);
            if (user == null)
                return new ApiResponse();
            var search = new Search
            {
                Title = addSearch.Title,
                userId = userId
            };
            _dbContext.Searches.Add(search);
            _dbContext.SaveChanges();
            return new ApiResponse(search, "Seccess");
        }

        public ApiResponse DeleteNotification(int userId, int notificationId)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.UID == userId);
            if (user == null)
                return new ApiResponse(user, "notFound");
            var notifaction = _dbContext.Notifications
                .FirstOrDefault(n => n.Id == notificationId & n.RecieverId == userId);
            if (notifaction == null)
                return new ApiResponse(notifaction , "notFound");

            _dbContext.Notifications.Remove(notifaction);
            _dbContext.SaveChanges();

            return new ApiResponse(notifaction , "Seccess");
        }

        public ApiResponse DeleteSearch(int userId ,int searchId)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.UID == userId);
            if (user == null)
                return new ApiResponse(user, "notFound");
            var search = _dbContext.Searches
                .FirstOrDefault(s => s.Id == searchId & s.userId == userId);
            if (search == null)
                return new ApiResponse(search, "notFound");

            _dbContext.Searches.Remove(search);
            _dbContext.SaveChanges();

            return new ApiResponse(search, "Seccess");
        }

        public ApiResponse ViewNotificationsList(int userId)
        {
            var notifications = _dbContext.Notifications
               .Where(s => s.Reciever.UID == userId)
               .ToList();
            if (notifications == null)
                return new ApiResponse(notifications, "notFound");

            return new ApiResponse(notifications , "Seccess");
        }

        public ApiResponse ViewSearchList(int userId)
        {
            var search = _dbContext.Searches
            .Where(s => s.User.UID == userId)
            .ToList();
            if (search == null)
                return new ApiResponse(search, "notFound");

            return new ApiResponse(search, "Seccess");
        }
    }
}
