using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace Model;

public partial class Fa25bearDbContext : DbContext
{
    public Fa25bearDbContext()
    {
    }

    public Fa25bearDbContext(DbContextOptions<Fa25bearDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BearAccount> BearAccounts { get; set; }

    public virtual DbSet<BearProfile> BearProfiles { get; set; }

    public virtual DbSet<BearType> BearTypes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    => optionsBuilder.UseSqlServer(GetConnectionString());

    private string GetConnectionString()
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        return config.GetConnectionString("DefaultConnection");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BearAccount>(entity =>
        {
            entity.HasKey(e => e.AccountId);

            entity.ToTable("BearAccount");

            entity.Property(e => e.AccountId).HasColumnName("AccountID");
            entity.Property(e => e.Email).HasMaxLength(150);
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.Password).HasMaxLength(100);
            entity.Property(e => e.Phone).HasMaxLength(50);
            entity.Property(e => e.UserName).HasMaxLength(50);
        });

        modelBuilder.Entity<BearProfile>(entity =>
        {
            entity.ToTable("BearProfile");

            entity.Property(e => e.BearName).HasMaxLength(150);
            entity.Property(e => e.CareNeeds).HasMaxLength(2000);
            entity.Property(e => e.Characteristics).HasMaxLength(2000);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

            entity.HasOne(d => d.BearType).WithMany(p => p.BearProfiles)
                .HasForeignKey(d => d.BearTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BearProfile_BearType");
        });

        modelBuilder.Entity<BearType>(entity =>
        {
            entity.ToTable("BearType");

            entity.Property(e => e.BearTypeName).HasMaxLength(250);
            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.Origin).HasMaxLength(250);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
