﻿// <auto-generated />
using System;
using GamesStoreWebApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GamesStoreWebApi.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    partial class ApplicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("GamesStoreWebApi.Models.Company", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("GamesStoreWebApi.Models.Game", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("DeveloperId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("PublisherId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("ReleaseDate")
                        .HasColumnType("date");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DeveloperId");

                    b.HasIndex("PublisherId");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("GamesStoreWebApi.Models.Price", b =>
                {
                    b.Property<Guid>("PricedGameId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("StartDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("date")
                        .HasDefaultValue(new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc));

                    b.Property<decimal>("Value")
                        .HasColumnType("money");

                    b.HasKey("PricedGameId", "StartDate");

                    b.ToTable("Price");
                });

            modelBuilder.Entity("GamesStoreWebApi.Models.Game", b =>
                {
                    b.HasOne("GamesStoreWebApi.Models.Company", "Developer")
                        .WithMany("DeveloperGames")
                        .HasForeignKey("DeveloperId");

                    b.HasOne("GamesStoreWebApi.Models.Company", "Publisher")
                        .WithMany("PublisherGames")
                        .HasForeignKey("PublisherId");

                    b.Navigation("Developer");

                    b.Navigation("Publisher");
                });

            modelBuilder.Entity("GamesStoreWebApi.Models.Price", b =>
                {
                    b.HasOne("GamesStoreWebApi.Models.Game", "PricedGame")
                        .WithMany("Prices")
                        .HasForeignKey("PricedGameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PricedGame");
                });

            modelBuilder.Entity("GamesStoreWebApi.Models.Company", b =>
                {
                    b.Navigation("DeveloperGames");

                    b.Navigation("PublisherGames");
                });

            modelBuilder.Entity("GamesStoreWebApi.Models.Game", b =>
                {
                    b.Navigation("Prices");
                });
#pragma warning restore 612, 618
        }
    }
}
