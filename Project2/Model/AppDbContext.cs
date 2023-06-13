using Microsoft.EntityFrameworkCore;
using Project2.Model.Entities;
//using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Project2.Model
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }
        public DbSet<User> Users { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Marketer> Marketers { get; set; }
        public DbSet<Deliver> Delivers { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Maintainer> Maintainers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Complaint> Complaints { get; set; }
        public DbSet<Interaction> Interactions { get; set; }
        public DbSet<Popularization> Popularizations { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<FollowingPage> FollowingPages { get; set; }
        public DbSet<Friend> Friends { get; set; }
        public DbSet<CustOrder> CustOrders { get; set; }
        public DbSet<CustPost> CustPosts { get; set; }
        public DbSet<DelivOrder> DelivOrders { get; set; }
        public DbSet<Search> Searches { get; set; }
        public DbSet<Auction> Auctions { get; set; }
        public DbSet<Cust_Auction> Cust_Auctions { get; set; }
        public DbSet<AuctionComm> AuctionComms { get; set; }
        public DbSet<Tender> Tenders { get; set; }
        public DbSet<Mark_Tender> Mark_Tenders { get; set; }
        public DbSet<TenderComm> TenderComms { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //user & customer //كل زبون هو مستخدم
            modelBuilder.Entity<User>()
                .HasOne(u => u.Customer)
                .WithOne(c => c.User)
                .HasForeignKey<Customer>(c => c.userId);
            //user & Marketer \\ كل تاجر هو مستخدم
            modelBuilder.Entity<User>()
                .HasOne(u => u.Marketer)
                .WithOne(m => m.User)
                .HasForeignKey<Marketer>(m => m.userId);
            //user & Deliver //كل شركة توصيل مستخدم
            modelBuilder.Entity<User>()
                .HasOne(u => u.Deliver)
                .WithOne(d => d.User)
                .HasForeignKey<Deliver>(d => d.userId);
            //customer & address \\ يملك الزبون عنوان وحيد
            modelBuilder.Entity<Customer>()
                .HasOne(c => c.Address)
                .WithMany()
                .HasForeignKey(c => c.AddressId);
            //marketer & address\\ يملك التاجر عنوان وحيد
            modelBuilder.Entity<Marketer>()
                .HasOne(m => m.Address)
                .WithMany()
                .HasForeignKey(m => m.AddressId);
            //deliver & add\\تملك شركة التوصيل عنوان وحيد
            modelBuilder.Entity<Deliver>()
                .HasOne(d => d.Address)
                .WithMany()
                .HasForeignKey(d => d.AddressId);
            //post & markter \\ كل تاجر يستطيع انشاء منشور او اكثر
            modelBuilder.Entity<Post>()
                .HasOne(p => p.Marketer)
                .WithMany(m => m.Posts)
                .HasForeignKey(p => p.marketerId);
            //post & product \\ المنشور يحوي على منتج واحد
            modelBuilder.Entity<Post>()
                .HasOne(p => p.Product)
                .WithMany()
                .HasForeignKey(p => p.productId);
            // متابعة التاجر
            modelBuilder.Entity<FollowingPage>()
                .HasOne(fp => fp.Marketer)
                .WithMany(m => m.FollowingPages)
                .HasForeignKey(fp => fp.pageId)
                .OnDelete(DeleteBehavior.Restrict);
            //following page & user\\متابعة من قبل المستخدم
            modelBuilder.Entity<FollowingPage>()
                .HasOne(fp => fp.User)
                .WithMany(u => u.FollowingPages)
                .HasForeignKey(fp => fp.userId)
                .OnDelete(DeleteBehavior.Restrict);
            //friend & customer\\متاابعة زبون
            modelBuilder.Entity<Friend>()
                .HasOne(f => f.Customer)
                .WithMany(c => c.Friends)
                .HasForeignKey(f => f.friendId)
                .OnDelete(DeleteBehavior.Restrict);
            //friend & user\\متابعة من قبل المستخدم
            modelBuilder.Entity<Friend>()
                .HasOne(f => f.User)
                .WithMany(u => u.Friends)
                .HasForeignKey(f => f.userId)
                .OnDelete(DeleteBehavior.Restrict);
            //marketer & Popularizations\\التاجر يقوم بترويج او اكثر
            modelBuilder.Entity<Marketer>()
               .HasMany(m => m.Popularizations)
               .WithOne(p => p.Marketer)
               .HasForeignKey(p => p.marketerId);
            //Product & Popularizations\\ الترويج يحو منتج واحد
            modelBuilder.Entity<Popularization>()
                .HasOne(p => p.Product)
                .WithMany()
                .HasForeignKey(p => p.productId);
            //order & post \\ يمكن ان يكون هناك طلب او اكثر من منشور واحد
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Post)
                .WithMany(p => p.Orders)
                .HasForeignKey(o => o.postId);
            //order & customer\\يطلب الزبون طلب او اكثر
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Customer)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.customerId);
            //order & deliver\\توصل شركة التوصيل طلب او اكثر
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Deliver)
                .WithMany(d => d.Orders)
                .HasForeignKey(o => o.deliverId);
            //customer & custorder \\يملك الزبون طلب شراء او اكثر
            modelBuilder.Entity<Customer>()
                .HasMany(c => c.CustOrders)
                .WithOne(co => co.Customer)
                .HasForeignKey(co => co.CustomerId);
            //order & custorder\\ طلبات شراء لنفس الاوردر
            modelBuilder.Entity<Order>()
                .HasMany(o => o.CustOrders)
                .WithOne(co => co.Order)
                .HasForeignKey(co => co.OrderId);
            //customer & CustPosts \\ يملك الزبون منشور او اكثر
            modelBuilder.Entity<Customer>()
                .HasMany(c => c.CustPosts)
                .WithOne(cp => cp.Customer)
                .HasForeignKey(cp => cp.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);
            //Post & CustPosts \\ يمكن ان تكون بوسات الزبون من نفس المنشور
            modelBuilder.Entity<Post>()
                .HasMany(p => p.CustPosts)
                .WithOne(cp => cp.Post)
                .HasForeignKey(cp => cp.PostId);
            //role & maint \\للمشرف وسم وظيفي واحد
            modelBuilder.Entity<Maintainer>()
                .HasOne(m => m.Role)
                .WithMany()
                .HasForeignKey(m => m.RoleId);
            // user & Interaction المستخدم يتفاعل مرة او اكثر
            modelBuilder.Entity<Interaction>()
                .HasOne(i => i.User)
                .WithMany(u => u.Interactions)
                .HasForeignKey(i => i.UserId);
            // Interaction & post \\ يملك المنشور تفاعل او اكثر
            modelBuilder.Entity<Interaction>()
                .HasOne(i => i.Post)
                .WithMany(p => p.Interactions)
                .HasForeignKey(i => i.PostId);
            // user & Searches \\ يملك المستخدم عملية بحث او اكثر
            modelBuilder.Entity<User>()
                .HasMany(u => u.Searches)
                .WithOne(s => s.User)
                .HasForeignKey(s => s.userId);
            //Complaint & \\ المستخدم يقدم شكوى او اكثر
            modelBuilder.Entity<Complaint>()
                .HasOne(c => c.Complainer)
                .WithMany(u => u.Complaints)
                .HasForeignKey(c => c.ComplainerId)
                .OnDelete(DeleteBehavior.Restrict);
            //Complaint \\ المستخدم يتلقى شكوى او اكثر
            modelBuilder.Entity<Complaint>()
                .HasOne(c => c.ComplainedOn)
                .WithMany(u => u.ComplaintsAgainst)
                .HasForeignKey(c => c.ComplainedOnId)
                .OnDelete(DeleteBehavior.Restrict);
            //DelivOrder & Deliver \\تملك شركة التوصيل طلب توصيل او اكثر
            modelBuilder.Entity<DelivOrder>()
                .HasOne(d => d.Deliver)
                .WithMany(d => d.DelivOrders)
                .HasForeignKey(d => d.DeliverId);
            //DelivOrder & Order الاورد يمكن ان يكون ب طلب توصيل او اكثر
            modelBuilder.Entity<DelivOrder>()
                .HasOne(d => d.Order)
                .WithMany(o => o.DelivOrders)
                .HasForeignKey(d => d.OrderId);
            //Auction & Marketer \\ التاجر يقوم بانشاء مزاد او اكثر
            modelBuilder.Entity<Auction>()
                .HasOne(a => a.Marketer)
                .WithMany(m => m.Auctions)
                .HasForeignKey(a => a.MarketerId)
                .OnDelete(DeleteBehavior.Restrict);
            //Cust_Auction & Customer \\يحوي المزاد زبون او اكثر
            modelBuilder.Entity<Cust_Auction>()
                .HasOne(ca => ca.Customer)
                .WithMany(c => c.CustAuctions)
                .HasForeignKey(ca => ca.customerId)
                .OnDelete(DeleteBehavior.Restrict);
            //Cust_Auction & Auction \\ينضم الزبون لمزاداو اكثر
            modelBuilder.Entity<Cust_Auction>()
                .HasOne(ca => ca.Auction)
                .WithMany(a => a.CustAuctions)
                .HasForeignKey(ca => ca.AuctionId)
                .OnDelete(DeleteBehavior.Restrict);
            //AuctionComm & Customer \\ يملك الزبون تعليق على المزاد 
            modelBuilder.Entity<AuctionComm>()
                .HasOne(ac => ac.Customer)
                .WithMany(c => c.AuctionComms)
                .HasForeignKey(ac => ac.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);
            //AuctionComm & Auction \\ المزاد يملك تعليق او اكثر
            modelBuilder.Entity<AuctionComm>()
                .HasOne(ac => ac.Auction)
                .WithMany(a => a.AuctionComments)
                .HasForeignKey(ac => ac.AuctionId);
            // Tender & Customer \\ ينشا الزبون مناقصة او اكثر
            modelBuilder.Entity<Tender>()
                .HasOne(t => t.Customer)
                .WithMany(c => c.Tenders)
                .HasForeignKey(t => t.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);
            // Mark_Tender & Marketer \\ ينصم التاجر الى مناقصة او اكثر
            modelBuilder.Entity<Mark_Tender>()
                .HasOne(mt => mt.Marketer)
                .WithMany(m => m.MarketerTenders)
                .HasForeignKey(mt => mt.MarketerId)
                .OnDelete(DeleteBehavior.Restrict);
            // Mark_Tender & Tender \\ تحوي المناقصة تاجر او اكثر
            modelBuilder.Entity<Mark_Tender>()
                .HasOne(mt => mt.Tender)
                .WithMany(t => t.Mark_Tenders)
                .HasForeignKey(mt => mt.TenderId)
                .OnDelete(DeleteBehavior.Restrict);
            // Marketer & TenderComm \\ يقوم التاجر بالتعليق مرة واحدة على المناقصة
            modelBuilder.Entity<TenderComm>()
                .HasOne(tc => tc.Marketer)
                .WithMany(m => m.TenderComments)
                .HasForeignKey(tc => tc.MarketerId)
                .OnDelete(DeleteBehavior.Restrict);
            //Tender & TenderComm \\ المناقصة تملك تعليق او اكثر
            modelBuilder.Entity<TenderComm>()
                .HasOne(tc => tc.Tender)
                .WithMany(t => t.TenderComments)
                .HasForeignKey(tc => tc.TenderId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
