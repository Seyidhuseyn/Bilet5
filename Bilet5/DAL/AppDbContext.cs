using Bilet5.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Bilet5.DAL
{
    public class AppDbContext:IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
    }
}
