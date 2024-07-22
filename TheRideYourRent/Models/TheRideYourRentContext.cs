using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace TheRideYourRent.Models;

public partial class TheRideYourRentContext : DbContext
{
    public TheRideYourRentContext()
    {
    }

    public TheRideYourRentContext(DbContextOptions<TheRideYourRentContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Car> Cars { get; set; }

    public virtual DbSet<CarBodyType> CarBodyTypes { get; set; }

    public virtual DbSet<CarMake> CarMakes { get; set; }

    public virtual DbSet<Driver> Drivers { get; set; }

    public virtual DbSet<Inspector> Inspectors { get; set; }

    public virtual DbSet<InspectorRegister> InspectorRegisters { get; set; }

    public virtual DbSet<Rental> Rentals { get; set; }

    public virtual DbSet<Return> Returns { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();
            //optionsBuilder.UseSqlServer(configuration.GetConnectionString("MyConnectionStringDev"));
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("MyConnectionStringAZURE"));
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Car>(entity =>
        {
            entity.HasKey(e => e.CarId).HasName("PK__Car__68A0340E451F0E26");

            entity.ToTable("Car");

            entity.HasIndex(e => e.CarNo, "UQ__Car__68A00DDCA282A880").IsUnique();

            entity.Property(e => e.CarId).HasColumnName("CarID");
            entity.Property(e => e.Available)
                .HasMaxLength(5)
                .IsUnicode(false);
            entity.Property(e => e.BodyTypeId).HasColumnName("BodyTypeID");
            entity.Property(e => e.CarNo)
                .HasMaxLength(8)
                .IsUnicode(false);
            entity.Property(e => e.KilometresTravelled).HasColumnName("Kilometres_Travelled");
            entity.Property(e => e.MakeId).HasColumnName("MakeID");
            entity.Property(e => e.Model)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.ServiceKilometres).HasColumnName("Service_Kilometres");

            entity.HasOne(d => d.BodyType).WithMany(p => p.Cars)
                .HasForeignKey(d => d.BodyTypeId)
                .HasConstraintName("FK__Car__BodyTypeID__3D5E1FD2");

            entity.HasOne(d => d.Make).WithMany(p => p.Cars)
                .HasForeignKey(d => d.MakeId)
                .HasConstraintName("FK__Car__MakeID__3C69FB99");
        });

        modelBuilder.Entity<CarBodyType>(entity =>
        {
            entity.HasKey(e => e.BodyTypeId).HasName("PK__CarBodyT__3F42D981D25DB25A");

            entity.ToTable("CarBodyType");

            entity.Property(e => e.BodyTypeId).HasColumnName("BodyTypeID");
            entity.Property(e => e.Description)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasColumnName("description");
        });

        modelBuilder.Entity<CarMake>(entity =>
        {
            entity.HasKey(e => e.MakeId).HasName("PK__CarMake__43646F3147C5C20E");

            entity.ToTable("CarMake");

            entity.Property(e => e.MakeId).HasColumnName("MakeID");
            entity.Property(e => e.Description)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasColumnName("description");
        });

        modelBuilder.Entity<Driver>(entity =>
        {
            entity.HasKey(e => e.DriverId).HasName("PK__Driver__F1B1CD243E614523");

            entity.ToTable("Driver");

            entity.Property(e => e.DriverId).HasColumnName("DriverID");
            entity.Property(e => e.Address)
                .HasMaxLength(80)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(30)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Inspector>(entity =>
        {
            entity.HasKey(e => e.InspectorId).HasName("PK__Inspecto__5FECC3FDCD753365");

            entity.ToTable("Inspector");

            entity.HasIndex(e => e.InspectorNo, "UQ__Inspecto__F49EBA96A483B770").IsUnique();

            entity.Property(e => e.InspectorId).HasColumnName("InspectorID");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.InspectorNo)
                .HasMaxLength(8)
                .IsUnicode(false)
                .HasColumnName("Inspector_No");
            entity.Property(e => e.Name)
                .HasMaxLength(30)
                .IsUnicode(false);
        });

        modelBuilder.Entity<InspectorRegister>(entity =>
        {
            entity.HasKey(e => e.InspectorId).HasName("PK__Inspecto__5FECC3FD69F89167");

            entity.ToTable("InspectorRegister");

            entity.Property(e => e.InspectorId).HasColumnName("InspectorID");
            entity.Property(e => e.Name)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Rental>(entity =>
        {
            entity.HasKey(e => e.RentalId).HasName("PK__Rental__97005963793ECE29");

            entity.ToTable("Rental");

            entity.Property(e => e.RentalId).HasColumnName("RentalID");
            entity.Property(e => e.CarNo)
                .HasMaxLength(8)
                .IsUnicode(false);
            entity.Property(e => e.DriverId).HasColumnName("DriverID");
            entity.Property(e => e.EndDate)
                .HasColumnType("date")
                .HasColumnName("End Date");
            entity.Property(e => e.InspectorNo)
                .HasMaxLength(8)
                .IsUnicode(false)
                .HasColumnName("Inspector_No");
            entity.Property(e => e.MakeId).HasColumnName("MakeID");
            entity.Property(e => e.RentalFee).HasColumnName("Rental_Fee");
            entity.Property(e => e.StartDate)
                .HasColumnType("date")
                .HasColumnName("Start Date");

            entity.HasOne(d => d.CarNoNavigation).WithMany(p => p.Rentals)
                .HasPrincipalKey(p => p.CarNo)
                .HasForeignKey(d => d.CarNo)
                .HasConstraintName("FK__Rental__CarNo__46E78A0C");

            entity.HasOne(d => d.Driver).WithMany(p => p.Rentals)
                .HasForeignKey(d => d.DriverId)
                .HasConstraintName("FK__Rental__DriverID__48CFD27E");

            entity.HasOne(d => d.InspectorNoNavigation).WithMany(p => p.Rentals)
                .HasPrincipalKey(p => p.InspectorNo)
                .HasForeignKey(d => d.InspectorNo)
                .HasConstraintName("FK__Rental__Inspecto__47DBAE45");

            entity.HasOne(d => d.Make).WithMany(p => p.Rentals)
                .HasForeignKey(d => d.MakeId)
                .HasConstraintName("FK__Rental__MakeID__49C3F6B7");
        });

        modelBuilder.Entity<Return>(entity =>
        {
            entity.HasKey(e => e.ReturnId).HasName("PK__Return__F445E9889DC78DA3");

            entity.ToTable("Return");

            entity.Property(e => e.ReturnId).HasColumnName("ReturnID");
            entity.Property(e => e.CarNo)
                .HasMaxLength(8)
                .IsUnicode(false);
            entity.Property(e => e.ElapsedDate).HasColumnName("Elapsed Date");
            entity.Property(e => e.InspectorNo)
                .HasMaxLength(8)
                .IsUnicode(false)
                .HasColumnName("Inspector_No");
            entity.Property(e => e.MakeId).HasColumnName("MakeID");
            entity.Property(e => e.ReturnDate)
                .HasColumnType("date")
                .HasColumnName("Return Date");

            entity.HasOne(d => d.CarNoNavigation).WithMany(p => p.Returns)
                .HasPrincipalKey(p => p.CarNo)
                .HasForeignKey(d => d.CarNo)
                .HasConstraintName("FK__Return__CarNo__4CA06362");

            entity.HasOne(d => d.Driver).WithMany(p => p.Returns)
                .HasForeignKey(d => d.DriverId)
                .HasConstraintName("FK__Return__DriverId__4E88ABD4");

            entity.HasOne(d => d.InspectorNoNavigation).WithMany(p => p.Returns)
                .HasPrincipalKey(p => p.InspectorNo)
                .HasForeignKey(d => d.InspectorNo)
                .HasConstraintName("FK__Return__Inspecto__4D94879B");

            entity.HasOne(d => d.Make).WithMany(p => p.Returns)
                .HasForeignKey(d => d.MakeId)
                .HasConstraintName("FK__Return__MakeID__4F7CD00D");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
