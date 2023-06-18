using Microsoft.EntityFrameworkCore;
using Project2.DTOs.CustomerDTOs;
using Project2.DTOs.DeliverDTOs;
using Project2.DTOs.MarketerDTOs;
using Project2.Model;
using Project2.Model.Entities;
using Project2.Model.Helpers;
using Project2.Repository.Interfaces;

namespace Project2.Repository.Services
{
    public class ProfileService : IProfileService
    {
        public AppDbContext _dbContext { get; set; }
        public ProfileService(AppDbContext DbContext)
        {
            _dbContext = DbContext;
        }

        public ApiResponse ViewCustomerProfile(int userId)
        {
            var customer = _dbContext.Customers
                    .Include(c => c.CustPosts)
                    .FirstOrDefault(c => c.userId == userId);
            if (customer == null)
                return new ApiResponse(customer, "notFound");

            return new ApiResponse(customer, "Seccess");
        }
        
        public ApiResponse ViewMarketerProfile(int userId)
        {
            var marketer = _dbContext.Marketers
                  .Include(m => m.Posts)
                  .FirstOrDefault(m => m.userId == userId);
            if (marketer == null)
                return new ApiResponse(marketer, "notFound");

            return new ApiResponse(marketer, "Seccess");
        }

        public ApiResponse ViewDeliverProfile(int userId)
        {
            var deliver = _dbContext.Delivers
                 .Include(d => d.DelivOrders)
                 .FirstOrDefault(d => d.userId == userId);
            if (deliver == null)
                return new ApiResponse(deliver, "notFound");

            return new ApiResponse(deliver, "Seccess");
        }

        public ApiResponse EditCustomerProfile(int userId, UpdateCustomerViewModel updateCustomer)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.UID == userId);
            if (user == null)
                return new ApiResponse(user, "notFound");

            var existingCustomer = _dbContext.Customers.FirstOrDefault(c => c.userId == userId);
            existingCustomer.Name = updateCustomer.Name;
            existingCustomer.phoneNumber = updateCustomer.phoneNumber;
            existingCustomer.photo = updateCustomer.photo;
            existingCustomer.Birthdate = updateCustomer.Birthdate;
            existingCustomer.city = updateCustomer.city;
            existingCustomer.zone = updateCustomer.zone;
            user.Name = updateCustomer.Name;
            _dbContext.SaveChanges();

            return new ApiResponse(existingCustomer, "Seccess");
        }

        public ApiResponse EditMarketerProfile(int userId, UpdateMarketerViewModel updateMarketer)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.UID == userId);
            if (user == null)
                return new ApiResponse(user, "notFound");

            var existingMarketer = _dbContext.Marketers.FirstOrDefault(c => c.userId == userId);
            existingMarketer.Name = updateMarketer.Name;
            existingMarketer.phoneNumber = updateMarketer.phoneNumber;
            existingMarketer.photo = updateMarketer.photo;
            existingMarketer.city = updateMarketer.city;
            existingMarketer.zone = updateMarketer.zone;
            user.Name = updateMarketer.Name;
            _dbContext.SaveChanges();

            return new ApiResponse(existingMarketer, "Seccess");
        }

        public ApiResponse EditDeliverProfile(int userId, UpdateDeliverViewModel updateDeliver)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.UID == userId);
            if (user == null)
                return new ApiResponse(user, "notFound");

            var existingDeliver = _dbContext.Delivers.FirstOrDefault(c => c.userId == userId);
            existingDeliver.Name = updateDeliver.Name;
            existingDeliver.phoneNumber = updateDeliver.phoneNumber;
            existingDeliver.photo = updateDeliver.photo;
            existingDeliver.city = updateDeliver.city;
            existingDeliver.zone = updateDeliver.zone;
            user.Name = updateDeliver.Name;
            _dbContext.SaveChanges();

            return new ApiResponse(existingDeliver, "Seccess");
        }

        public ApiResponse ViewFollowingPagesList(int userId)
        {
            var followingPages = _dbContext.Users
                .Include(u => u.FollowingPages)
                .Where(u => u.UID == userId).ToList();
            if (followingPages == null)
                return new ApiResponse(followingPages, "notFound");
            return new ApiResponse(followingPages, "Seccess");
        }

        public ApiResponse ViewFriendsList(int userId)
        {
            var custfriend = _dbContext.Customers
                .Include(cf => cf.Friends)
                .Where(cf => cf.userId == userId).ToList();
            if (custfriend == null)
                return new ApiResponse(custfriend, "notFound");
            return new ApiResponse(custfriend, "Seccess");
        }

        public ApiResponse FollowMarketer(int userId, int marketerId)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.UID == userId);
            if (user == null)
                return new ApiResponse(user, "UsernotFound");
            var marketer = _dbContext.Marketers.FirstOrDefault(m => m.Id == marketerId);
            if(marketer == null)
                return new ApiResponse(marketer, "MarketernotFound");
            var followingPage = new FollowingPage
            {
                pageId = marketerId,
                userId = userId,
                CreatedDate = DateTime.Now
            };

            _dbContext.FollowingPages.Add(followingPage);
            _dbContext.SaveChanges();
            user.FollowingPages.Add(followingPage);

            return new ApiResponse(followingPage , "Seccess");
        }

        public ApiResponse UnfollowMarketer(int userId, int marketerId)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.UID == userId);
            var followingPage = _dbContext.FollowingPages
                .FirstOrDefault(fp => fp.pageId == marketerId && fp.userId == userId);

            if (followingPage != null && user != null)
            {
                user.FollowingPages.Remove(followingPage);
                _dbContext.FollowingPages.Remove(followingPage);
                _dbContext.SaveChanges();
                return new ApiResponse(followingPage, "Seccess");
            }
            return new ApiResponse(followingPage , "notFound");
        }

        public ApiResponse FollowCustomer(int userId, int customerId)
        {
            var customer = _dbContext.Customers.FirstOrDefault(c => c.userId == userId);
            if (customer == null)
                return new ApiResponse(customer, "UsernotFound");
            var frindcustomer = _dbContext.Customers.FirstOrDefault(c => c.Id == customerId);
            if (frindcustomer == null)
                return new ApiResponse(frindcustomer, "MarketernotFound");
            var friend = new Friend
            {
                friendId = customerId,
                userId = userId,
                CreatedDate = DateTime.Now
            };

            _dbContext.Friends.Add(friend);
            _dbContext.SaveChanges();
            customer.Friends.Add(friend);

            return new ApiResponse(friend, "Seccess");
        }

        public ApiResponse UnfollowCustomer(int userId, int customerId)
        {
            var customer = _dbContext.Customers.FirstOrDefault(c => c.userId == userId);
            var friends = _dbContext.Friends
                .FirstOrDefault(fp => fp.friendId == customerId && fp.userId == userId);

            if (friends != null && customer != null)
            {
                customer.Friends.Remove(friends);
                _dbContext.Friends.Remove(friends);
                _dbContext.SaveChanges();
                return new ApiResponse(friends, "Seccess");
            }
            return new ApiResponse(friends, "notFound");
        }

        public ApiResponse ContractWithDeliver(int userId, int deliverId)
        {
            var marketer = _dbContext.Marketers.FirstOrDefault(c => c.userId == userId);
            if (marketer == null)
                return new ApiResponse(marketer, "MarketernotFound");
            var deliver = _dbContext.Delivers.FirstOrDefault(c => c.Id == deliverId);
            if (deliver == null)
                return new ApiResponse(deliver, "DeliverernotFound");
            //if(marketer.Delivers.)
           
            marketer.Delivers.Add(deliver);

            _dbContext.SaveChanges();
            return new ApiResponse(deliver, "Seccess");
        }

        public ApiResponse CancelContractWithDeliver(int userId, int deliverId)
        {
            var marketer = _dbContext.Marketers.FirstOrDefault(c => c.userId == userId);
            if (marketer == null)
                return new ApiResponse(marketer, "MarketernotFound");
            var deliver = _dbContext.Delivers.FirstOrDefault(c => c.Id == deliverId);
            if (deliver == null)
                return new ApiResponse(deliver, "DeliverernotFound");

            marketer.Delivers.Remove(deliver);

            _dbContext.SaveChanges();
            return new ApiResponse(deliver, "Seccess");
        }

        public ApiResponse AddAgent(int userId, int customerId)
        {
            var marketer = _dbContext.Marketers.FirstOrDefault(c => c.userId == userId);
            if (marketer == null)
                return new ApiResponse(marketer, "MarketernotFound");
            var customer = _dbContext.Customers.FirstOrDefault(c => c.Id == customerId);
            if (customer == null)
                return new ApiResponse(customer, "CustomernotFound");
            //if(marketer.Delivers.)

            marketer.CustomersAgents.Add(customer);

            _dbContext.SaveChanges();
            return new ApiResponse(customer, "Seccess");
        }

        public ApiResponse RemoveAgent(int userId, int customerId)
        {
            var marketer = _dbContext.Marketers.FirstOrDefault(c => c.userId == userId);
            if (marketer == null)
                return new ApiResponse(marketer, "MarketernotFound");
            var customer = _dbContext.Customers.FirstOrDefault(c => c.Id == customerId);
            if (customer == null)
                return new ApiResponse(customer, "CustomernotFound");
            //if(marketer.Delivers.)

            marketer.CustomersAgents.Remove(customer);

            _dbContext.SaveChanges();
            return new ApiResponse(customer, "Seccess");
        }
    }

}
