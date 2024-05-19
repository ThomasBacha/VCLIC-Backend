using Microsoft.EntityFrameworkCore;
using VCLICApi.Model;

namespace VCLICApi.Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options)
            : base(options)
        {
        }

        public DbSet<BetaBlockerValueSet> BetaBlockerValueSets { get; set; } = null!;
        public DbSet<Medication> Medications { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BetaBlockerValueSet>()
                .HasKey(b => b.ValueSetId); // Define ValueSetId as the primary key

            base.OnModelCreating(modelBuilder);
        }
    }
}
