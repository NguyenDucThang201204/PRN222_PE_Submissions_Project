using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PE_PRN232_FA25_TranNguyenVietQuang_BE.Repository.Entities;

public partial class FA25BearDBContext : DbContext
{
    public FA25BearDBContext()
    {
    }

    public FA25BearDBContext(DbContextOptions<FA25BearDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BearAccount> BearAccounts { get; set; }

    public virtual DbSet<BearProfile> BearProfiles { get; set; }

    public virtual DbSet<BearType> BearTypes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-FNKTHSN\\DAVIDSQLEXPRESS;Initial Catalog=FA25BearDB;User ID=sa;Password=12345;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BearAccount>(entity =>
        {
            entity.HasKey(e => e.AccountId).HasName("PK__BearAcco__349DA58659B02A12");

            entity.ToTable("BearAccount");

            entity.Property(e => e.AccountId)
                .ValueGeneratedNever()
                .HasColumnName("AccountID");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FullName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UserName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<BearProfile>(entity =>
        {
            entity.HasKey(e => e.BearProfileId).HasName("PK__BearProf__17E64CD743264EF1");

            entity.ToTable("BearProfile");

            entity.Property(e => e.BearProfileId)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.BearName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.BearTypeId)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CareNeeds)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Characteristics)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

            entity.HasOne(d => d.BearType).WithMany(p => p.BearProfiles)
                .HasForeignKey(d => d.BearTypeId)
                .HasConstraintName("FK_BearProfile_BearType");
        });

        modelBuilder.Entity<BearType>(entity =>
        {
            entity.HasKey(e => e.BearTypeId).HasName("PK__BearType__ECEEBC142F0A60BC");

            entity.ToTable("BearType");

            entity.Property(e => e.BearTypeId)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.BearTypeName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Description)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Origin)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
