using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using EfrashBatek.ViewModel;

namespace EfrashBatek.Models
{
    public class Context:IdentityDbContext<User>
    {
        public Context() { }
        public Context(DbContextOptions options) : base(options)
        { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer("Data Source =DESKTOP-V1G3BTU\\SQLEXPRESS; Initial Catalog = EfrashBatek; Integrated Security = True");

            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cart_Item>().HasKey(x => new { x.CartID, x.ItemID });
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Custom> Customs { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Cart_Item>Cart_Items { get; set; }
        public DbSet<Customer>Customers { get; set; }
        public DbSet<Design> Designs { get; set; }
        public DbSet<Designer> designers { get; set; }
        public DbSet<Feedback> feedbacks { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Order_Item> Order_Items { get; set; }
        public DbSet<Photo>Photos { get; set; }
        public DbSet<Product>Products { get; set; }
        public DbSet<Shop>Shops { get; set; }
        public DbSet<Staff> The_Staff { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Video> Videos { get; set; }
        public DbSet<Warrantly_Request>Warrantl_Requests { get; set; }
        public DbSet<WishList> WishLists { get; set; }
        public DbSet<Admin> Admin { get; set; }
       public  DbSet<Contact_Us> Contact_Us { get; set; }
       public DbSet<EfrashBatek.ViewModel.ForgetPasswordVM> ForgetPasswordVM { get; set; }
       public DbSet<EfrashBatek.ViewModel.ResetPasswordVM> ResetPasswordVM { get; set; }
       public DbSet<WishListItem> WishListItems { get; set; }
    }
}
