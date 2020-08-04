﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using XTMData;

namespace XTMData.Migrations
{
    [DbContext(typeof(XTMDbContext))]
    [Migration("20200804192636_initialCreate")]
    partial class initialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("XTMCore.Avion", b =>
                {
                    b.Property<int>("PlaneID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Available")
                        .HasColumnType("bit");

                    b.Property<bool>("Catering")
                        .HasColumnType("bit");

                    b.Property<int>("FuelCapacity")
                        .HasColumnType("int");

                    b.Property<int>("KmCost")
                        .HasColumnType("int");

                    b.Property<int>("MaxVelocity")
                        .HasColumnType("int");

                    b.Property<int>("PassengerCapacity")
                        .HasColumnType("int");

                    b.Property<string>("PlaneName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PlaneType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PropulsionType")
                        .HasColumnType("int");

                    b.Property<bool>("Wifi")
                        .HasColumnType("bit");

                    b.HasKey("PlaneID");

                    b.ToTable("Planes");
                });

            modelBuilder.Entity("XTMCore.Booking", b =>
                {
                    b.Property<int>("BookingID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Date")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DestinyCity")
                        .HasColumnType("int");

                    b.Property<int>("OriginCity")
                        .HasColumnType("int");

                    b.Property<int>("Passengers")
                        .HasColumnType("int");

                    b.Property<int>("PlaneID")
                        .HasColumnType("int");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("BookingID");

                    b.ToTable("Bookings");
                });

            modelBuilder.Entity("XTMCore.Client", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Location")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserID");

                    b.ToTable("Clients");
                });
#pragma warning restore 612, 618
        }
    }
}