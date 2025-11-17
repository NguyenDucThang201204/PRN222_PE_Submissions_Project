using System;
using System.Collections.Generic;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DAL;

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

    private string GetConnectionString()
    {
        IConfiguration config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", true, true)
            .Build();
        var strConn = config["ConnectionStrings:MyCnn"];

        return strConn;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(GetConnectionString());

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BearAccount>(entity =>
        {
            entity.HasKey(e => e.AccountId).HasName("PK__BearAcco__349DA5868DF034AE");

            entity.ToTable("BearAccount");

            entity.HasIndex(e => e.FullName, "UQ__BearAcco__89C60F11FA56A817").IsUnique();

            entity.Property(e => e.AccountId)
                .ValueGeneratedNever()
                .HasColumnName("AccountID");
            entity.Property(e => e.Email).HasMaxLength(80);
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.Password).HasMaxLength(80);
            entity.Property(e => e.Phone).HasMaxLength(80);
            entity.Property(e => e.UserName).HasMaxLength(80);
        });

        modelBuilder.Entity<BearProfile>(entity =>
        {
            entity.HasKey(e => e.BearProfileId).HasName("PK__BearProf__17E64CD7B4F4D99E");

            entity.ToTable("BearProfile");

            entity.Property(e => e.BearProfileId).ValueGeneratedNever();
            entity.Property(e => e.BearName).HasMaxLength(200);
            entity.Property(e => e.BearWeight).HasMaxLength(220);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

            entity.HasOne(d => d.BearType).WithMany(p => p.BearProfiles)
                .HasForeignKey(d => d.BearTypeId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__BearProfi__BearT__4F7CD00D");
        });

        modelBuilder.Entity<BearType>(entity =>
        {
            entity.HasKey(e => e.BearTypeId).HasName("PK__BearType__ECEEBC14D059B14A");

            entity.ToTable("BearType");

            entity.Property(e => e.BearTypeId).ValueGeneratedNever();
            entity.Property(e => e.BearTypeName).HasMaxLength(80);
            entity.Property(e => e.Description).HasMaxLength(150);
            entity.Property(e => e.Origin).HasMaxLength(60);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
