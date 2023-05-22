using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EfrashBatek.Models
{
    public class Context:IdentityDbContext
    {

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

        DbSet<Address> Addresses { get; set; }
        DbSet<Brand> Brands { get; set; }
        DbSet<Custom> Customs { get; set; }
        DbSet<Cart> Carts { get; set; }
        DbSet<Cart_Item>Cart_Items { get; set; }
        DbSet<Customer>Customers { get; set; }
        DbSet<Design> Designs { get; set; }
        DbSet<Designer> designers { get; set; }
        DbSet<Feedback> feedbacks { get; set; }
        DbSet<Item> Items { get; set; }
        DbSet<Order> Orders { get; set; }
        DbSet<Order_Item> Order_Items { get; set; }
        DbSet<Photo>Photos { get; set; }
        DbSet<Product>Products { get; set; }
        DbSet<Shop>Shops { get; set; }
        DbSet<Staff> The_Staff { get; set; }
        DbSet<User> Users { get; set; }
        DbSet<Video> Videos { get; set; }
        DbSet<Warrantly_Request>Warrantl_Requests { get; set; }
        DbSet<WishList> WishLists { get; set; }
        DbSet<Admin> Admin { get; set; }
    }
}
