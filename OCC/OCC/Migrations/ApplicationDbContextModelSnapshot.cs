﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OCC.Models;

namespace OCC.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.14-servicing-32113")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("OCC.Models.AvailabilityCleaner", b =>
                {
                    b.Property<long>("AvailabilityCleanerId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("CleanerId");

                    b.Property<string>("EndTime");

                    b.Property<DateTime>("StartTime");

                    b.HasKey("AvailabilityCleanerId");

                    b.ToTable("AvailabilityCleaners");
                });

            modelBuilder.Entity("OCC.Models.Cleaner", b =>
                {
                    b.Property<long>("CleanerId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Afternoon");

                    b.Property<double>("BankAccount");

                    b.Property<string>("Certificate");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<bool>("Evening");

                    b.Property<string>("ExperienceLevel")
                        .IsRequired();

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<bool>("IsCleaner");

                    b.Property<string>("Location")
                        .IsRequired();

                    b.Property<bool>("Morning");

                    b.Property<bool>("Night");

                    b.Property<bool>("Weekends");

                    b.HasKey("CleanerId");

                    b.ToTable("Cleaners");
                });

            modelBuilder.Entity("OCC.Models.Customer", b =>
                {
                    b.Property<long>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .IsRequired();

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("FullName")
                        .IsRequired();

                    b.Property<string>("Phone")
                        .IsRequired();

                    b.HasKey("CustomerId");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("OCC.Models.Order", b =>
                {
                    b.Property<long>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("CleanerId");

                    b.Property<long>("CustomerId");

                    b.Property<int>("Duration");

                    b.Property<string>("Location");

                    b.Property<string>("OrderPaymentState");

                    b.Property<DateTime>("ServiceDay");

                    b.Property<long>("ServiceId");

                    b.Property<string>("ShiftTime");

                    b.HasKey("OrderId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("OCC.Models.Service", b =>
                {
                    b.Property<long>("ServiceId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("Price");

                    b.Property<string>("Type");

                    b.HasKey("ServiceId");

                    b.ToTable("Services");
                });
#pragma warning restore 612, 618
        }
    }
}
