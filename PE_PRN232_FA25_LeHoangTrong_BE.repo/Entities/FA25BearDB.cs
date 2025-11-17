using Microsoft.EntityFrameworkCore;

namespace PE_PRN232_FA25_LeHoangTrong_BE.repo.Entities;

public partial class FA25BearDB : DbContext
{
    public FA25BearDB()
    {
    }

    public FA25BearDB(DbContextOptions<FA25BearDB> options)
        : base(options)
    {
    }

    public virtual DbSet<BearAccount> BearAccounts { get; set; }

    public virtual DbSet<BearProfile> BearProfiles { get; set; }

    public virtual DbSet<BearType> BearTypes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BearAccount>(entity =>
        {
            entity.HasKey(e => e.AccountId);

            entity.ToTable("BearAccount");

            entity.Property(e => e.Email)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.Password)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.Phone)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.UserName)
                .HasMaxLength(10)
                .IsFixedLength();
        });

        modelBuilder.Entity<BearProfile>(entity =>
        {
            entity.ToTable("BearProfile");

            entity.Property(e => e.BearName)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.BearWeight).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.CareNeeds)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.Characteristics)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.ModifiedDate)
                .HasMaxLength(10)
                .IsFixedLength();

            entity.HasOne(d => d.BearType).WithMany(p => p.BearProfiles)
                .HasForeignKey(d => d.BearTypeId)
                .HasConstraintName("FK_BearProfile_BearType");
        });

        modelBuilder.Entity<BearType>(entity =>
        {
            entity.ToTable("BearType");

            entity.Property(e => e.BearTypeName)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.Description)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.Origin)
                .HasMaxLength(10)
                .IsFixedLength();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
