﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using SqliteAdapter.Model;
using System;

namespace SqliteAdapter.Migrations
{
    [DbContext(typeof(RnvScotlandYardContext))]
    partial class RnvScotlandYardContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452");

            modelBuilder.Entity("SqliteAdapter.Model.GameSessionDb", b =>
                {
                    b.Property<string>("GameSessionId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("MaxPoliceOfficers");

                    b.Property<string>("Name");

                    b.Property<DateTimeOffset>("StartTime");

                    b.HasKey("GameSessionId");

                    b.ToTable("GameSessions");
                });

            modelBuilder.Entity("SqliteAdapter.Model.MoveDb", b =>
                {
                    b.Property<int>("MoveId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("MrxDbMrxId");

                    b.Property<string>("MrxDbMrxId1");

                    b.Property<string>("PoliceOfficerDbPoliceOfficerId");

                    b.Property<string>("StationId");

                    b.Property<string>("VehicleType");

                    b.HasKey("MoveId");

                    b.HasIndex("MrxDbMrxId");

                    b.HasIndex("MrxDbMrxId1");

                    b.HasIndex("PoliceOfficerDbPoliceOfficerId");

                    b.HasIndex("StationId");

                    b.ToTable("Moves");
                });

            modelBuilder.Entity("SqliteAdapter.Model.MrxDb", b =>
                {
                    b.Property<string>("MrxId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("GameSessionDbId");

                    b.Property<string>("LastKnownStationStationId");

                    b.Property<string>("Name");

                    b.HasKey("MrxId");

                    b.HasIndex("GameSessionDbId")
                        .IsUnique();

                    b.HasIndex("LastKnownStationStationId");

                    b.ToTable("MrXs");
                });

            modelBuilder.Entity("SqliteAdapter.Model.PoliceOfficerDb", b =>
                {
                    b.Property<string>("PoliceOfficerId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CurrentStationStationId");

                    b.Property<string>("GameSessionDbId");

                    b.Property<string>("Name");

                    b.HasKey("PoliceOfficerId");

                    b.HasIndex("CurrentStationStationId");

                    b.HasIndex("GameSessionDbId");

                    b.ToTable("PoliceOfficers");
                });

            modelBuilder.Entity("SqliteAdapter.Model.StationDb", b =>
                {
                    b.Property<string>("StationId")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("Latitude");

                    b.Property<double>("Longitude");

                    b.Property<string>("Name");

                    b.Property<string>("StationType");

                    b.HasKey("StationId");

                    b.ToTable("Stations");
                });

            modelBuilder.Entity("SqliteAdapter.Model.MoveDb", b =>
                {
                    b.HasOne("SqliteAdapter.Model.MrxDb")
                        .WithMany("MoveHistory")
                        .HasForeignKey("MrxDbMrxId");

                    b.HasOne("SqliteAdapter.Model.MrxDb")
                        .WithMany("OpenMoves")
                        .HasForeignKey("MrxDbMrxId1");

                    b.HasOne("SqliteAdapter.Model.PoliceOfficerDb")
                        .WithMany("MoveHistory")
                        .HasForeignKey("PoliceOfficerDbPoliceOfficerId");

                    b.HasOne("SqliteAdapter.Model.StationDb", "Station")
                        .WithMany()
                        .HasForeignKey("StationId");
                });

            modelBuilder.Entity("SqliteAdapter.Model.MrxDb", b =>
                {
                    b.HasOne("SqliteAdapter.Model.GameSessionDb")
                        .WithOne("Mrx")
                        .HasForeignKey("SqliteAdapter.Model.MrxDb", "GameSessionDbId");

                    b.HasOne("SqliteAdapter.Model.StationDb", "LastKnownStation")
                        .WithMany()
                        .HasForeignKey("LastKnownStationStationId");
                });

            modelBuilder.Entity("SqliteAdapter.Model.PoliceOfficerDb", b =>
                {
                    b.HasOne("SqliteAdapter.Model.StationDb", "CurrentStation")
                        .WithMany()
                        .HasForeignKey("CurrentStationStationId");

                    b.HasOne("SqliteAdapter.Model.GameSessionDb", "GameSessionDb")
                        .WithMany("PoliceOfficers")
                        .HasForeignKey("GameSessionDbId");
                });
#pragma warning restore 612, 618
        }
    }
}
