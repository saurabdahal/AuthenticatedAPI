using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AuthentationWebAPI.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }

        
        public AppDbContext(DbContextOptions<AppDbContext> options)
                    : base(options)
        {
        }
        
    }
}
