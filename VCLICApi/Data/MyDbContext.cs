using Microsoft.EntityFrameworkCore;

namespace VCLICApi.Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options)
            : base(options)
        {
        }

        public DbSet<ValueSet> ValueSets { get; set; }  // This DbSet represents the ValueSets table in the database.

        // If you have more entities, define them here like:
        // public DbSet<AnotherEntity> AnotherEntities { get; set; }
    }
}
