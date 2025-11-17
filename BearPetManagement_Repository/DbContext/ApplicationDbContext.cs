using BearPetManagement_Repository.Models;
using BearPetManagement_Repository.NewFolder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BearPetManagement_Repository.ApplicationDatabaseContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {

        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<BearAccount> BearAccounts { get; set; }
        public DbSet<BearProfile> BearProfiles { get; set; }
        public DbSet<BearType> BearTypes { get; set; }

        public static string GetConnectionString(string connectionStringName)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            string connectionString = config.GetConnectionString(connectionStringName);
            return connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlServer(GetConnectionString("DefaultConnection")).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BearAccount>(entity =>
            {
                entity.HasKey(e => e.AccountID).HasName("PK_Account");
                entity.Property(e => e.AccountID).ValueGeneratedOnAdd();
            }
            );


            modelBuilder.Entity<BearProfile>(entity =>
            {
                entity.HasKey(e => e.BearProfileId).HasName("PK_BearProfile");
                entity.Property(e => e.BearProfileId).ValueGeneratedOnAdd();
                entity.HasOne(e => e.BearType)
                .WithMany(bt => bt.BearProfiles)
                .HasForeignKey(e => e.BearProfileId)
                .HasConstraintName("FK_BearProfile_BearType");
            });

            modelBuilder.Entity<BearType>(entity =>
            {
                entity.HasKey(e => e.BearTypeId).HasName("PK_BearType");
                entity.Property(e => e.BearTypeId).ValueGeneratedOnAdd();

                entity.HasMany(bt => bt.BearProfiles)
                .WithOne(b => b.BearType);
            });


        }

    }
}
