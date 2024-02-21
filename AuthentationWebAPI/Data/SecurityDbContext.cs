using System.Collections.Generic;
using System.Data;

namespace AuthentationWebAPI.Data
{
    public class SecurityDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Configure your database connection here
            optionsBuilder.UseSqlServer("YourConnectionString"); // Replace with your actual database connection string
        }
    }
}
