﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Project2.Model.Entities
{
    public class Marketer
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int phoneNumber { get; set; }
        public string? photo { get; set; }
        public int? Point { get; set; }
        public int userId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string city { get; set; }
        public string zone { get; set; }

        [ForeignKey("userId")]
        public User User { get; set; }
        public ICollection<Post> Posts { get; set; }
        public ICollection<Customer> CustomersAgents { get; set; }
        public ICollection<Popularization> Popularizations { get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<Deliver> Delivers { get; set; }
        public ICollection<Auction> Auctions { get; set; }
        public ICollection<Mark_Tender> MarketerTenders { get; set; }
        public ICollection<TenderComm> TenderComments { get; set; }
        public ICollection<FollowingPage> FollowingPages { get; set; }

    }
}
