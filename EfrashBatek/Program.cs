using EfrashBatek.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EfrashBatek
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
            DataSeeding();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
        
    public static void DataSeeding()
    {
        using var context = new Context();
        context.Database.EnsureCreated();
          
        var Product = context.Products.FirstOrDefault(x=>x.ID==1);
            var customer = context.Customers.FirstOrDefault(x => x.Id == 1);
            if(customer == null)
            {

                User user = new User { Id = "11111", FirstName = "alyaa", LastName = "elhawary", Email = "alyaamamoon@gmail.com", PhoneNumber = "01111111" };
                context.Customers.Add(new Customer { Id = 1, User = user});
            }
        if(Product==null)
        {
           // context.Products.Add(new Product { ID=1,ProductName= (ProductName)1, Description="Ikea is Very good " });
            //context.Products.Add(new Product { ID = 2, ProductName = (ProductName)2, Description = "Wayfair is very good " });
            //context.Products.Add(new Product { ID = 3, ProductName = (ProductName)3, Description = "Room is Very good " });
        }
            var brand = context.Brands.FirstOrDefault(x => x.ID == 1);
            if (brand == null)
            {
                context.Brands.Add(new Brand { ID = 1, Name = "Addidas", Category=(Category)1});
                context.Brands.Add(new Brand { ID = 2, Name = "mesh aerf", Category = (Category)2 });
                context.Brands.Add(new Brand { ID = 3, Name = "tabaan ya capten", Category = (Category)3 });
            }
            var shop = context.Shops.FirstOrDefault(x => x.ID == 1);
            if(shop==null)
            {
                context.Shops.Add(new Shop { ID = 1, Name = "Nssagon", ShopAddress = "Qena",PhoneNumber="01094248766"});
                context.Shops.Add(new Shop { ID = 2, Name = "Furniture",ShopAddress = "Cairo" ,PhoneNumber="01550632066"});
                context.Shops.Add(new Shop { ID = 3, Name = "manarh",ShopAddress="Assiut",PhoneNumber="01205442521"});
            }

         var item = context.Items.FirstOrDefault(x => x.ID == 1);
           if (item == null)
            {
                context.Items.Add(new Item { ID = 1, Name = "serar", Brand_ID = 1, Code = "6548", Price = 5000, ShopID = 2, ProductID = 2, Image = "0.jpg",Description="yotuu",QuantityInStore=3 });
                context.Items.Add(new Item { ID = 2, Name ="chair", Brand_ID =2,Code="87865",Price=5000,ShopID=2,ProductID=2,Image="1.jpg", Description = "yotuu", QuantityInStore = 3 });
                context.Items.Add(new Item { ID = 3, Name = "trtr", Brand_ID =2, Code = "7969", Price = 5000, ShopID = 2, ProductID = 2, Image = "2.jpg", Description = "yotuu", QuantityInStore = 3 });
            }


            context.SaveChanges();
    }
}
    }
