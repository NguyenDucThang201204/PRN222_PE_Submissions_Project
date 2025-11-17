using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace BO;

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
        => optionsBuilder.UseSqlServer(GetConnection());


    protected String GetConnection()
    {
        IConfiguration config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json").Build();
        return config["ConnectionStrings:DBConnect"];
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BearAccount>(entity =>
        {
            entity.HasKey(e => e.AccountId).HasName("PK__BearAcco__349DA586475A5418");

            entity.ToTable("BearAccount");

            entity.HasIndex(e => e.Emal, "UQ__BearAcco__DCB4F375FC1A9B82").IsUnique();

            entity.Property(e => e.AccountId).HasColumnName("AccountID");
            entity.Property(e => e.Emal).HasMaxLength(100);
            entity.Property(e => e.FullName).HasMaxLength(80);
            entity.Property(e => e.Password).HasMaxLength(100);
            entity.Property(e => e.Phone).HasMaxLength(80);
            entity.Property(e => e.UserName).HasMaxLength(80);
        });

        modelBuilder.Entity<BearProfile>(entity =>
        {
            entity.HasKey(e => e.BearProfileId).HasName("PK__BearProf__17E64CD71D671700");

            entity.ToTable("BearProfile");

            entity.Property(e => e.BearName).HasMaxLength(200);
            entity.Property(e => e.CareNeeds).HasMaxLength(200);
            entity.Property(e => e.Characteristics).HasMaxLength(2000);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

            entity.HasOne(d => d.BearType).WithMany(p => p.BearProfiles)
                .HasForeignKey(d => d.BearTypeId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_Bear_BearType");
        });

        modelBuilder.Entity<BearType>(entity =>
        {
            entity.HasKey(e => e.BearTypeId).HasName("PK__BearType__ECEEBC146C2B4A32");

            entity.ToTable("BearType");

            entity.Property(e => e.BearTypeName).HasMaxLength(200);
            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.Origin).HasMaxLength(200);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
