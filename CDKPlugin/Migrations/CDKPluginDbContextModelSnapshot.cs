﻿// <auto-generated />
using System;
using CDKPlugin.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CDKPlugin.Migrations
{
    [DbContext(typeof(CDKPluginDbContext))]
    partial class CDKPluginDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.31")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("CDKPlugin.Entities.CDKData", b =>
                {
                    b.Property<Guid>("CKey")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.HasKey("CKey");

                    b.ToTable("Zengyj_CDKPlugin_CdkData");
                });

            modelBuilder.Entity("CDKPlugin.Entities.LogData", b =>
                {
                    b.Property<Guid>("CKey")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("RedeemedTime")
                        .HasColumnType("datetime(6)");

                    b.Property<ulong>("SteamID")
                        .HasColumnType("bigint unsigned");

                    b.HasKey("CKey");

                    b.HasIndex("CKey", "SteamID")
                        .IsUnique();

                    b.ToTable("Zengyj_CDKPlugin_LogData");
                });
#pragma warning restore 612, 618
        }
    }
}
