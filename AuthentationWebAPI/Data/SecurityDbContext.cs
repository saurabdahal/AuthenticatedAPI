using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AuthentationWebAPI.Data
{
    public class SecurityDbContext : IdentityDbContext<IdentityUser>
    {
        
        public SecurityDbContext(DbContextOptions<SecurityDbContext> options)
            : base(options)
        {
        }
        

        /*
        public SecurityDbContext() { }

        // referecen for database migration
        public SecurityDbContext(DbContextOptions<SecurityDbContext> options) : base(options) => Database.Migrate();

        //database configuration
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlite();
        */
    }
}

