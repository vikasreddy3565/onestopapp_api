using Microsoft.EntityFrameworkCore;
using OneStopApp_Api.EntityFramework.Model;
namespace OneStopApp_Api.EntityFramework.Data
{
    public class OsaDbContext : DbContext
    {
        public OsaDbContext(DbContextOptions<OsaDbContext> options) : base(options)
        { }
        public DbSet<User> Users { get; set; }
        public DbSet<UserStatus> UserStatuses { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<ClientValidation> ClientValidations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
