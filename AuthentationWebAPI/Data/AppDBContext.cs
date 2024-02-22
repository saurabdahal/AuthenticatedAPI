using Microsoft.EntityFrameworkCore;

namespace AuthentationWebAPI.Data
{
    public class AppDBContext : DbContext
    {
        public DbSet<AppDBContext> appDBContexts { get; set; }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }

        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) => Database.Migrate();

        //database configuration
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlite();
    }
}
