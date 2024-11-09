using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TripProject.Models;

public partial class TripsContext : DbContext
{
    public TripsContext()
    {
    }

    public TripsContext(DbContextOptions<TripsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TbCategory> TbCategories { get; set; }

    public virtual DbSet<TbCity> TbCities { get; set; }

    public virtual DbSet<TbCountry> TbCountries { get; set; }

    public virtual DbSet<TbRequest> TbRequests { get; set; }

    public virtual DbSet<TbTrip> TbTrips { get; set; }

    public virtual DbSet<VwTrip> VwTrips { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-32RHGLK;Database=Trips;Trusted_Connection=True;TrustServerCertificate=Yes");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TbCategory>(entity =>
        {
            entity.ToTable("TbCategory");

            entity.Property(e => e.CategoryName).HasMaxLength(50);
        });

        modelBuilder.Entity<TbCity>(entity =>
        {
            entity.ToTable("TbCity");

            entity.Property(e => e.CityName).HasMaxLength(50);

            entity.HasOne(d => d.Countury).WithMany(p => p.TbCities)
                .HasForeignKey(d => d.CounturyId)
                .HasConstraintName("FK_TbCity_TbCountry");
        });

        modelBuilder.Entity<TbCountry>(entity =>
        {
            entity.ToTable("TbCountry");

            entity.Property(e => e.CounturyName).HasMaxLength(50);
        });

        modelBuilder.Entity<TbRequest>(entity =>
        {
            entity.ToTable("TbRequest");

            entity.Property(e => e.Gmail).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(10);
            entity.Property(e => e.Phone).HasMaxLength(11);

            entity.HasOne(d => d.TbTrips).WithMany(p => p.TbRequests)
                .HasForeignKey(d => d.RequestId)
                .HasConstraintName("FK_TbRequest_TbTrip");
        });

        modelBuilder.Entity<TbTrip>(entity =>
        {
            entity.ToTable("TbTrip");

            entity.Property(e => e.Description).HasMaxLength(2000);
            entity.Property(e => e.Image).HasMaxLength(400);
            entity.Property(e => e.Name).HasMaxLength(50);

            entity.HasOne(d => d.Category).WithMany(p => p.TbTrips)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK_TbTrip_TbCategory");

            entity.HasOne(d => d.City).WithMany(p => p.TbTrips)
                .HasForeignKey(d => d.CityId)
                .HasConstraintName("FK_TbTrip_TbCity");
        });

        modelBuilder.Entity<VwTrip>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vwTrip");

            entity.Property(e => e.CategoryName).HasMaxLength(50);
            entity.Property(e => e.CityName).HasMaxLength(50);
            entity.Property(e => e.Description).HasMaxLength(2000);
            entity.Property(e => e.Image).HasMaxLength(400);
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
