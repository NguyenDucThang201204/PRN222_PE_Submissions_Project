using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Repositories.Models;

namespace Repositories
{
    public partial class BearManagementDbContext : DbContext
    {
        public BearManagementDbContext()
        {
        }


        public BearManagementDbContext(DbContextOptions<BearManagementDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<BearProfile> BearProfiles { get; set; }
        public virtual DbSet<BearType> BearTypes { get; set; }
        public virtual DbSet<BearAccount> BearAccounts { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BearType>(entity =>
            {
                entity.HasKey(e => e.BearTypeId).HasName("PK__BearType__DAD4F3BE33E09654");

                entity.ToTable("BearType");

                entity.Property(e => e.BearTypeId)
                    .ValueGeneratedNever()
                    .HasColumnName("BearTypeId");
                entity.Property(e => e.BearTypeName)
                    .HasMaxLength(255)
                    .IsUnicode(false);
                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .IsUnicode(false);
                entity.Property(e => e.Origin)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<BearProfile>(entity =>
            {
                entity.HasKey(e => e.BearProfileId).HasName("PK__BearProfile__785BD69FF3D8930C");

                entity.ToTable("BearProfile");

                entity.Property(e => e.BearProfileId)
                    .ValueGeneratedNever()
                    .HasColumnName("BearProfileID");
                entity.Property(e => e.BearTypeId).HasColumnName("BearTypeId");
                entity.Property(e => e.BearWeight)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.Characteristics)
                    .HasMaxLength(100)
                    .IsUnicode(false);
                entity.Property(e => e.BearName)
                    .HasMaxLength(255)
                    .IsUnicode(false);
                entity.Property(e => e.CareNeeds)
                    .HasMaxLength(100)
                    .IsUnicode(false);
                entity.Property(e => e.ModifiedDate).HasColumnType("date");

                entity.HasOne(d => d.BearType).WithMany(p => p.BearProfiles)
                    .HasForeignKey(d => d.BearTypeId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_BearProfile_brand");
            });

            modelBuilder.Entity<BearAccount>(entity =>
            {
                entity.HasKey(e => e.AccountId).HasName("PK__BearAc__349DA58674FFF6D7");

                entity.Property(e => e.AccountId)
                    .ValueGeneratedNever()
                    .HasColumnName("AccountID");
                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .IsUnicode(false);
                entity.Property(e => e.Password)
                    .HasMaxLength(255)
                    .IsUnicode(false);
                entity.Property(e => e.UserName)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
