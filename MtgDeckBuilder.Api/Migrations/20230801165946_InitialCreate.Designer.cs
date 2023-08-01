﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MtgDeckBuilder.Api.Data;

#nullable disable

namespace MtgDeckBuilder.Api.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20230801165946_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MtgDeckBuilder.Api.Data.DeckItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("DeckItems");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Golgari Saprolings"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Grixis Fun Stuff"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
