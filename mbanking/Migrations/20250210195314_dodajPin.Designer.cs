﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebTemplate.Models;

#nullable disable

namespace WebTemplate.Migrations
{
    [DbContext(typeof(BankaContext))]
    [Migration("20250210195314_dodajPin")]
    partial class dodajPin
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("WebTemplate.Models.Korisnik", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<int?>("RacunId")
                        .HasColumnType("int");

                    b.Property<string>("adresa")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("brojTelefona")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ime")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("jmbg")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("nvarchar(13)");

                    b.Property<string>("prezime")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.HasIndex("RacunId")
                        .IsUnique()
                        .HasFilter("[RacunId] IS NOT NULL");

                    b.ToTable("Korisnici");
                });

            modelBuilder.Entity("WebTemplate.Models.Racun", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("brojRacuna")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("sredstva")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("valuta")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("Racuni");
                });

            modelBuilder.Entity("WebTemplate.Models.Korisnik", b =>
                {
                    b.HasOne("WebTemplate.Models.Racun", "Racun")
                        .WithOne("Korisnik")
                        .HasForeignKey("WebTemplate.Models.Korisnik", "RacunId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Racun");
                });

            modelBuilder.Entity("WebTemplate.Models.Racun", b =>
                {
                    b.Navigation("Korisnik");
                });
#pragma warning restore 612, 618
        }
    }
}
