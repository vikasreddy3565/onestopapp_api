using Microsoft.EntityFrameworkCore;
using OneStopApp_Api.EntityFramework.Model;

namespace OneStopApp_Api.EntityFramework.Data
{
    public class OsaLogContext : DbContext
    {
        public OsaLogContext(DbContextOptions<OsaLogContext> options) : base(options)
        { }

        public DbSet<EventLog> EventLogs { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //ErrorLog
            modelBuilder.Entity<EventLog>().ToTable("EventLog");
        }

    }
}