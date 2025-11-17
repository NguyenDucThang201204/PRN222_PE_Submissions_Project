using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BOs;

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
        //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        //        => optionsBuilder.UseSqlServer("Server=(local);database = FA25BearDB;uid=sa;pwd=1234567890;trustservercertificate=true");

        => optionsBuilder.UseSqlServer(GetConnectionString());
    private string GetConnectionString()
    {
        return new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build()
            .GetConnectionString("FA25BearDB");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BearAccount>(entity =>
        {
            entity.HasKey(e => e.AccountId);

            entity.ToTable("BearAccount");

            entity.Property(e => e.AccountId).HasMaxLength(50);
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.FullName).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(50);
            entity.Property(e => e.Phone).HasMaxLength(50);
            entity.Property(e => e.RoleId).HasMaxLength(50);
            entity.Property(e => e.Username).HasMaxLength(50);
        });

        modelBuilder.Entity<BearProfile>(entity =>
        {
            entity.ToTable("BearProfile");

            entity.Property(e => e.BearProfileId).HasMaxLength(50);
            entity.Property(e => e.BearName).HasMaxLength(50);
            entity.Property(e => e.BearTypeId).HasMaxLength(50);
            entity.Property(e => e.CareNeeds).HasMaxLength(50);
            entity.Property(e => e.Characteristics).HasMaxLength(50);

            entity.HasOne(d => d.BearType).WithMany(p => p.BearProfiles)
                .HasForeignKey(d => d.BearTypeId)
                .HasConstraintName("FK_BearProfile_BearType");
        });

        modelBuilder.Entity<BearType>(entity =>
        {
            entity.ToTable("BearType");

            entity.Property(e => e.BearTypeId).HasMaxLength(50);
            entity.Property(e => e.BearTypeName).HasMaxLength(50);
            entity.Property(e => e.Description).HasMaxLength(50);
            entity.Property(e => e.Origin).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
