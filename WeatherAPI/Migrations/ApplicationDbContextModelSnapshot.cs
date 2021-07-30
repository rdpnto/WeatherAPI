﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WeatherAPI.Data;

namespace WeatherAPI.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.8");

            modelBuilder.Entity("WeatherAPI.Models.Weather", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Local")
                        .HasColumnType("longtext");

                    b.Property<double>("Temp_max")
                        .HasColumnType("double");

                    b.Property<double>("Temp_min")
                        .HasColumnType("double");

                    b.Property<double>("Temp_now")
                        .HasColumnType("double");

                    b.HasKey("Id");

                    b.ToTable("WeatherLog");
                });
#pragma warning restore 612, 618
        }
    }
}
