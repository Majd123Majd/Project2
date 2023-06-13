﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project2.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int phoneNumber { get; set; }
        public int age { get; set; }
        public int AddressId { get; set; }
        public string? photo { get; set; }
        public string? AccountCach { get; set; }
        public int? Point { get; set; }
        public int userId { get; set; }
        public DateTime CreatedDate { get; set; }

        [ForeignKey("AddressId")]
        public Address Address { get; set; }
        
        [ForeignKey("userId")]
        public User User { get; set; }
        public ICollection<Order>? Orders { get; set; }
        public ICollection<CustOrder>? CustOrders { get; set; }
        public ICollection<CustPost>? CustPosts { get; set; }
        public ICollection<Cust_Auction>? CustAuctions { get; set; }
        public ICollection<Tender>? Tenders { get; set; }
        public ICollection<Friend>? Friends { get; set; }
        public ICollection<AuctionComm>? AuctionComms { get; set; }
    }
}