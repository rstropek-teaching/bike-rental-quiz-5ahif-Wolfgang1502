﻿// <auto-generated />
using BikeRental.Contexts;
using BikeRental.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace BikeRental.Migrations
{
    [DbContext(typeof(BikeRentalContext))]
    [Migration("20180301194718_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BikeRental.Models.Bike", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Brand")
                        .IsRequired()
                        .HasMaxLength(25);

                    b.Property<int>("Category");

                    b.Property<DateTime>("LastService")
                        .HasColumnType("date");

                    b.Property<string>("Notes")
                        .HasMaxLength(1000);

                    b.Property<double>("PriceAdditionalHour");

                    b.Property<double>("PriceFirstHour");

                    b.Property<DateTime>("PurchaseDate")
                        .HasColumnType("date");

                    b.HasKey("ID");

                    b.ToTable("Bikes");
                });

            modelBuilder.Entity("BikeRental.Models.Customer", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Birthday")
                        .HasColumnType("date");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int>("Gender");

                    b.Property<string>("HouseNumber")
                        .HasMaxLength(10);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(75);

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasMaxLength(75);

                    b.Property<string>("Town")
                        .IsRequired()
                        .HasMaxLength(75);

                    b.Property<string>("ZipCode")
                        .IsRequired()
                        .HasMaxLength(10);

                    b.HasKey("ID");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("BikeRental.Models.Rental", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BikeID");

                    b.Property<int>("CustomerID");

                    b.Property<bool>("Paid");

                    b.Property<DateTime>("RentalBegin");

                    b.Property<DateTime?>("RentalEnd");

                    b.Property<double>("TotalCosts");

                    b.HasKey("ID");

                    b.HasIndex("BikeID");

                    b.HasIndex("CustomerID");

                    b.ToTable("Rentals");
                });

            modelBuilder.Entity("BikeRental.Models.Rental", b =>
                {
                    b.HasOne("BikeRental.Models.Bike", "Bike")
                        .WithMany()
                        .HasForeignKey("BikeID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BikeRental.Models.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
