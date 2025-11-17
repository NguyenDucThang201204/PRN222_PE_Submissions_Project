using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Repository.Models;
using System;
using System.Collections.Generic;

namespace Repository.Repos;

public partial class Fa25bearDbContext : DbContext
{
    public Fa25bearDbContext()
    {
    }

    public Fa25bearDbContext(DbContextOptions<Fa25bearDbContext> options)
        : base(options)
    {
    }

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


    public virtual DbSet<BearAccount> BearAccounts { get; set; }

    public virtual DbSet<BearProfile> BearProfiles { get; set; }

    public virtual DbSet<BearType> BearTypes { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Server=localhost,1433;Database=FA25BearDB;User Id=sa;Password=12345;Encrypt=False;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BearAccount>(entity =>
        {
            entity.HasKey(e => e.AccountId).HasName("PK__BearAcco__349DA5865C8130BF");

            entity.ToTable("BearAccount");

            entity.Property(e => e.AccountId)
                .ValueGeneratedNever()
                .HasColumnName("AccountID");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.FullName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.UserName)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<BearProfile>(entity =>
        {
            entity.HasKey(e => e.BearProfileId).HasName("PK__BearProf__17E64CF71594FCE1");

            entity.ToTable("BearProfile");

            entity.Property(e => e.BearProfileId)
                .ValueGeneratedNever()
                .HasColumnName("BearProfileID");
            entity.Property(e => e.BearName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.BearTypeId).HasColumnName("BearTypeID");
            entity.Property(e => e.CareNeeds)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Characteristics)
                .HasMaxLength(255)
                .IsUnicode(false);

            //entity.HasOne(d => d.BearType).WithMany(p => p.BearProfiles)
            //    .HasForeignKey(d => d.BearTypeId)
            //    .HasConstraintName("FK__BearProfi__BearT__3B75D760");
        });

        modelBuilder.Entity<BearType>(entity =>
        {
            entity.HasKey(e => e.BearTypeId).HasName("PK__BearType__ECEEBC3425B60DE6");

            entity.ToTable("BearType");

            entity.Property(e => e.BearTypeId)
                .ValueGeneratedNever()
                .HasColumnName("BearTypeID");
            entity.Property(e => e.BearTypeName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
