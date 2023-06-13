﻿using Project2.Model.DTOs.UserDTOs;

namespace Project2.Model.DTOs.DeliverDTOs
{
    public class AddDeliverViewModel
    {
        public string Name { get; set; }
        public int phoneNumber { get; set; }
        public int AddressId { get; set; }
        public string? photo { get; set; }
        public string? AccountCach { get; set; }
        public AddUserViewModel UserInformation { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
