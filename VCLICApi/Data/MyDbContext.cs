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

        public DbSet<ValueSet> ValueSets { get; set; }
        public DbSet<BetaBlockerValueSet> BetaBlockerValueSets { get; set; }
        public DbSet<Medication> Medications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<BetaBlockerValueSet>()
                .HasKey(b => b.ValueSetId);

            modelBuilder.Entity<Medication>()
                .HasKey(m => m.MedicationId);
        }
    }
}
