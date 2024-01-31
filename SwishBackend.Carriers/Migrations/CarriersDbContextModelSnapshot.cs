﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SwishBackend.Carriers.Data;

#nullable disable

namespace SwishBackend.Carriers.Migrations
{
    [DbContext(typeof(CarriersDbContext))]
    partial class CarriersDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SwishBackend.Carriers.Models.NearestServicePointResponse.CordinateDTO", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ServicePointId")
                        .HasColumnType("int");

                    b.Property<string>("countryCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("easting")
                        .HasColumnType("float");

                    b.Property<double>("northing")
                        .HasColumnType("float");

                    b.Property<string>("srId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ServicePointId");

                    b.ToTable("CordinateDTO");
                });

            modelBuilder.Entity("SwishBackend.Carriers.Models.NearestServicePointResponse.DHLGeoDTO", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<double>("Latitude")
                        .HasColumnType("float");

                    b.Property<double>("Longitude")
                        .HasColumnType("float");

                    b.Property<int>("ServicePointId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ServicePointId")
                        .IsUnique();

                    b.ToTable("DHLGeoDTO");
                });

            modelBuilder.Entity("SwishBackend.Carriers.Models.NearestServicePointResponse.DeliveryAddressDTO", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PostalCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ServicePointId")
                        .HasColumnType("int");

                    b.Property<string>("StreetName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StreetNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ServicePointId")
                        .IsUnique();

                    b.ToTable("DeliveryAddressDTO");
                });

            modelBuilder.Entity("SwishBackend.Carriers.Models.NearestServicePointResponse.OpeningHoursDTO", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ServicePointId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ServicePointId")
                        .IsUnique();

                    b.ToTable("OpeningHoursDTO");
                });

            modelBuilder.Entity("SwishBackend.Carriers.Models.NearestServicePointResponse.PostNordNearestServicePoint", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ServicePointId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("PostNordNearestServicePoints");
                });

            modelBuilder.Entity("SwishBackend.Carriers.Models.NearestServicePointResponse.PostalServiceDTO", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CloseDay")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CloseTime")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OpenDay")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OpenTime")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OpeningHoursId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OpeningHoursId");

                    b.ToTable("PostalServiceDTO");
                });

            modelBuilder.Entity("SwishBackend.Carriers.Models.NearestServicePointResponse.CordinateDTO", b =>
                {
                    b.HasOne("SwishBackend.Carriers.Models.NearestServicePointResponse.PostNordNearestServicePoint", "ServicePoint")
                        .WithMany("Coordinates")
                        .HasForeignKey("ServicePointId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ServicePoint");
                });

            modelBuilder.Entity("SwishBackend.Carriers.Models.NearestServicePointResponse.DHLGeoDTO", b =>
                {
                    b.HasOne("SwishBackend.Carriers.Models.NearestServicePointResponse.PostNordNearestServicePoint", "ServicePoint")
                        .WithOne("Geo")
                        .HasForeignKey("SwishBackend.Carriers.Models.NearestServicePointResponse.DHLGeoDTO", "ServicePointId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ServicePoint");
                });

            modelBuilder.Entity("SwishBackend.Carriers.Models.NearestServicePointResponse.DeliveryAddressDTO", b =>
                {
                    b.HasOne("SwishBackend.Carriers.Models.NearestServicePointResponse.PostNordNearestServicePoint", "ServicePoint")
                        .WithOne("DeliveryAddress")
                        .HasForeignKey("SwishBackend.Carriers.Models.NearestServicePointResponse.DeliveryAddressDTO", "ServicePointId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ServicePoint");
                });

            modelBuilder.Entity("SwishBackend.Carriers.Models.NearestServicePointResponse.OpeningHoursDTO", b =>
                {
                    b.HasOne("SwishBackend.Carriers.Models.NearestServicePointResponse.PostNordNearestServicePoint", "ServicePoint")
                        .WithOne("OpeningHours")
                        .HasForeignKey("SwishBackend.Carriers.Models.NearestServicePointResponse.OpeningHoursDTO", "ServicePointId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ServicePoint");
                });

            modelBuilder.Entity("SwishBackend.Carriers.Models.NearestServicePointResponse.PostalServiceDTO", b =>
                {
                    b.HasOne("SwishBackend.Carriers.Models.NearestServicePointResponse.OpeningHoursDTO", "OpeningHours")
                        .WithMany("PostalServices")
                        .HasForeignKey("OpeningHoursId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("OpeningHours");
                });

            modelBuilder.Entity("SwishBackend.Carriers.Models.NearestServicePointResponse.OpeningHoursDTO", b =>
                {
                    b.Navigation("PostalServices");
                });

            modelBuilder.Entity("SwishBackend.Carriers.Models.NearestServicePointResponse.PostNordNearestServicePoint", b =>
                {
                    b.Navigation("Coordinates");

                    b.Navigation("DeliveryAddress");

                    b.Navigation("Geo");

                    b.Navigation("OpeningHours");
                });
#pragma warning restore 612, 618
        }
    }
}
