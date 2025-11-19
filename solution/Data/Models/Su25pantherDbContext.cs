using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Data.Models;

public partial class Su25pantherDbContext : DbContext
{
    public Su25pantherDbContext()
    {
    }

    public Su25pantherDbContext(DbContextOptions<Su25pantherDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<PantherAccount> PantherAccounts { get; set; }

    public virtual DbSet<PantherProfile> PantherProfiles { get; set; }

    public virtual DbSet<PantherType> PantherTypes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;Database=SU25PantherDB;Uid=sa;Pwd=Hellkaisers123.;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PantherAccount>(entity =>
        {
            entity.HasKey(e => e.AccountId);

            entity.ToTable("PantherAccount");

            entity.Property(e => e.AccountId).HasColumnName("AccountID");
            entity.Property(e => e.Email).HasMaxLength(150);
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.Password).HasMaxLength(100);
            entity.Property(e => e.Phone).HasMaxLength(50);
            entity.Property(e => e.UserName).HasMaxLength(50);
        });

        modelBuilder.Entity<PantherProfile>(entity =>
        {
            entity.ToTable("PantherProfile");

            entity.Property(e => e.Characteristics).HasMaxLength(2000);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.PantherName).HasMaxLength(150);
            entity.Property(e => e.Warning).HasMaxLength(1500);

            entity.HasOne(d => d.PantherType).WithMany(p => p.PantherProfiles)
                .HasForeignKey(d => d.PantherTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PantherProfile_PantherType");
        });

        modelBuilder.Entity<PantherType>(entity =>
        {
            entity.ToTable("PantherType");

            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.Origin).HasMaxLength(250);
            entity.Property(e => e.PantherTypeName).HasMaxLength(250);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
