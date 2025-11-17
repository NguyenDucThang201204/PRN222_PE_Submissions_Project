using Microsoft.EntityFrameworkCore;
using PE_PRN232_FA25_LeCongHung_DAL.Entities;

namespace PE_PRN232_FA25_LeCongHung_DAL.Data
{
    public class BearDbContext : DbContext
    {
        public BearDbContext(DbContextOptions<BearDbContext> options) : base(options)
        {
        }

        public DbSet<BearType> BearTypes { get; set; }
        public DbSet<BearProfile> BearProfiles { get; set; }
        public DbSet<BearAccount> BearAccounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure BearType
            modelBuilder.Entity<BearType>(entity =>
            {
                entity.HasKey(e => e.BearTypeId);
                entity.Property(e => e.BearTypeName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Origin).HasMaxLength(100);
                entity.Property(e => e.Description).HasMaxLength(500);
            });

            // Configure BearProfile
            modelBuilder.Entity<BearProfile>(entity =>
            {
                entity.HasKey(e => e.BearProfileId);
                entity.Property(e => e.BearName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.BearWeight).HasColumnType("decimal(10,2)");
                entity.Property(e => e.Characteristics).HasMaxLength(500);
                entity.Property(e => e.CareNeeds).HasMaxLength(500);
                entity.Property(e => e.ModifiedDate).HasDefaultValueSql("GETDATE()");

                entity.HasOne(e => e.BearType)
                    .WithMany(bt => bt.BearProfiles)
                    .HasForeignKey(e => e.BearTypeId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Configure BearAccount
            modelBuilder.Entity<BearAccount>(entity =>
            {
                entity.HasKey(e => e.AccountId);
                entity.Property(e => e.UserName).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Password).IsRequired().HasMaxLength(255);
                entity.Property(e => e.FullName).HasMaxLength(100);
                entity.Property(e => e.Email).HasMaxLength(100);
                entity.Property(e => e.Phone).HasMaxLength(20);

                entity.HasIndex(e => e.UserName).IsUnique();
            });
        }
    }
}

