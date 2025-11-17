using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace FA25Bear.DataAccess.Models;

public partial class Fa25bearDbContext : DbContext
{
    private readonly IConfiguration _configuration;
    public Fa25bearDbContext()
    {
    }

    public Fa25bearDbContext(DbContextOptions<Fa25bearDbContext> options, IConfiguration configuration)
        : base(options)
    {
        _configuration = configuration;
    }

    public virtual DbSet<BearAccount> BearAccounts { get; set; }

    public virtual DbSet<BearProfile> BearProfiles { get; set; }

    public virtual DbSet<BearType> BearTypes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var builder = new ConfigurationBuilder()
                                 .SetBasePath(Directory.GetCurrentDirectory())
                                 .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            IConfigurationRoot configuration = builder.Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("ConnectionStrings"));
        }
    }
        //=> optionsBuilder.UseSqlServer("Server=DESKTOP-G1AJRSK;Database=FA25BearDB;User ID=sa;Password=12345678;Encrypt=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BearAccount>(entity =>
        {
            entity.HasKey(e => e.AccountId).HasName("PK__BearAcco__349DA586E19BD910");

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
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Username)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<BearProfile>(entity =>
        {
            entity.HasKey(e => e.BearProfileId).HasName("PK__BearProf__17E64CF700F239DB");

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

            entity.HasOne(d => d.BearType).WithMany(p => p.BearProfiles)
                .HasForeignKey(d => d.BearTypeId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_bearprofile_beartype");
        });

        modelBuilder.Entity<BearType>(entity =>
        {
            entity.HasKey(e => e.BearTypeId).HasName("PK__BearType__ECEEBC3468647365");

            entity.ToTable("BearType");

            entity.Property(e => e.BearTypeId)
                .ValueGeneratedNever()
                .HasColumnName("BearTypeID");
            entity.Property(e => e.BearTypeName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Descriptionn)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Origin)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
