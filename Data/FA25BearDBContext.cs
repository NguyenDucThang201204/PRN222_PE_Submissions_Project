using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class FA25BearDBContext : DbContext
    {
        public FA25BearDBContext(DbContextOptions<FA25BearDBContext> options) : base(options) { }

        public DbSet<BearProfile> BearProfiles { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<BearProfile>(entity =>
            {
                entity.HasKey(e => e.BearProfileId);
                entity.Property(e => e.BearName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.BearWeight).IsRequired();
                entity.Property(e => e.Characteristics).HasMaxLength(500);
                entity.Property(e => e.CareNeeds).HasMaxLength(500);
                entity.Property(e => e.ModifiedDate).IsRequired().HasDefaultValueSql("GETDATE()");
            });
        }


    }
}
