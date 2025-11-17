using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

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

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(GetConnectionString());

    private string GetConnectionString()
    {
        IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true).Build();
        return configuration["ConnectionStrings:DefaultConnectionString"];
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BearAccount>(entity =>
        {
            entity.HasKey(e => e.AccountId).HasName("PK__BearAcco__349DA58659039C93");

            entity.ToTable("BearAccount");

            entity.HasIndex(e => e.Email, "UQ__BearAcco__A9D105343CB6459A").IsUnique();

            entity.HasIndex(e => e.UserName, "UQ__BearAcco__C9F284565DDBC4E9").IsUnique();

            entity.Property(e => e.AccountId).HasColumnName("AccountID");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FullName).HasMaxLength(150);
            entity.Property(e => e.Password).HasMaxLength(255);
            entity.Property(e => e.Phone).HasMaxLength(20);
            entity.Property(e => e.UserName).HasMaxLength(50);
        });

        modelBuilder.Entity<BearProfile>(entity =>
        {
            entity.HasKey(e => e.BearProfileId).HasName("PK__BearProf__17E64CD78CA9CA58");

            entity.ToTable("BearProfile");

            entity.Property(e => e.BearName).HasMaxLength(100);
            entity.Property(e => e.BearWeight).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.ModifiedDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.BearType).WithMany(p => p.BearProfiles)
                .HasForeignKey(d => d.BearTypeId)
                .HasConstraintName("FK_BearProfile_BearType");
        });

        modelBuilder.Entity<BearType>(entity =>
        {
            entity.HasKey(e => e.BearTypeId).HasName("PK__BearType__ECEEBC1435031EC7");

            entity.ToTable("BearType");

            entity.Property(e => e.BearTypeName).HasMaxLength(100);
            entity.Property(e => e.Origin).HasMaxLength(150);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
