using EfrashBatek.Controllers;
using EfrashBatek.Models;
using EfrashBatek.service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EfrashBatek
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();//For used Controller and Views
            services.AddDbContext<Context>(options => options.
           UseSqlServer(Configuration.GetConnectionString("DATA")));
            services.AddIdentity<User, IdentityRole> (options =>
        {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequiredLength = 8;

                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
            }).AddEntityFrameworkStores<Context>().AddDefaultTokenProviders();
            services.AddScoped<UserManager<User>>();
            services.AddScoped<IAddressRepository, AddressRepository>();
			services.AddScoped<IUserRepository, UserRepository>();
			services.AddScoped<IAdminRepository, AdminRepository>();
            services.AddScoped<IBrandRepository,BrandRepository>();
            services.AddScoped<ICartRepository, CartRepository>();
            services.AddScoped<ICart_ItemRepository, Cart_ItemRepository>();
            services.AddScoped<ICustomRepository,CustomRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IDesignerRepository, DesignerRepository>();
            services.AddScoped<IDesignRepository, DesignRepository>();
            services.AddScoped<IFeedbackRepository, FeedbackRepository>();
            services.AddScoped<IItemRepository, ItemRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrder_ItemRepository, Order_ItemRepository>();
            services.AddScoped<IPhotoRepository, PhotoRepository>();
            services.AddScoped<IShopRepository, ShopRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IStaffRepository,StaffRepository>();
            services.AddScoped<IWarrantly_RequestRepository, Warrantly_RequestRepository>();
            services.AddScoped<IVideoRepository, VideoRepository>();
            services.AddScoped<IWishListRepository, WishListRepository>();
            services.AddScoped<IContact_UsRepository, Contact_UsRepository>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            /*Session*/
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(15);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();//static file

            app.UseRouting();
            app.UseAuthentication();

            app.UseAuthorization();
            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=TrendingProducts}/{id?}");//When Open Web Open Bydefault Home 
            });
        }
    }
}
