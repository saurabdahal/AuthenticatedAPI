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
        
        /*
        public AppDbContext() { }

        // referecen for database migration
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) => Database.Migrate();

        //database configuration
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlite();
        */
    }
}
