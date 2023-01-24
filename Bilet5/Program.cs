using Bilet5.DAL;
using Bilet5.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Bilet5
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<AppDbContext>(opt =>
            {
                opt.UseSqlServer(builder.Configuration.GetConnectionString("MSSQL"));
            });

            builder.Services.AddIdentity<AppUser, IdentityRole>(opt =>
            {
                opt.Password.RequireDigit = true;
                opt.Password.RequireNonAlphanumeric = true;
                opt.Password.RequiredLength = 5;
                opt.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                opt.Lockout.AllowedForNewUsers = true;
            }).AddDefaultTokenProviders().AddEntityFrameworkStores<AppDbContext>();

            var app = builder.Build();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(name: "areas", pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

            app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}