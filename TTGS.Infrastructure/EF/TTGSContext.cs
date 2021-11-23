using TTGS.Shared.Entity;
using Microsoft.EntityFrameworkCore;

namespace TTGS.Infrastructure.EF
{
    public class TTGSContext : DbContext
    {

        public TTGSContext(DbContextOptions<TTGSContext> contextOptions) : base(contextOptions)
        { }

        public DbSet<Communication> Communications { get; set; }
        public DbSet<Order> Orders { get; set; }

#if DEBUG
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging(true);
            base.OnConfiguring(optionsBuilder);
        }
#endif

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
