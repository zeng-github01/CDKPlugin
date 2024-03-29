﻿// <auto-generated />
using System;
using CDKPlugin.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CDKPlugin.Migrations
{
    [DbContext(typeof(CDKPluginDbContext))]
    [Migration("20230907034538_AddMaxRedeem")]
    partial class AddMaxRedeem
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.32")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("CDKPlugin.Entities.CDKData", b =>
                {
                    b.Property<string>("CKey")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<uint>("Experience")
                        .HasColumnType("int unsigned");

                    b.Property<int>("MaxRedeem")
                        .HasColumnType("int");

                    b.Property<decimal>("Money")
                        .HasColumnType("decimal(65,30)");

                    b.Property<string>("PermissionRoleID")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("Reputation")
                        .HasColumnType("int");

                    b.Property<ushort>("Vehicle")
                        .HasColumnType("smallint unsigned");

                    b.HasKey("CKey");

                    b.ToTable("CDKData");
                });

            modelBuilder.Entity("CDKPlugin.Entities.LogData", b =>
                {
                    b.Property<int>("LogID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("CDKDataCKey")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("CDKey")
                        .IsRequired()
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<DateTime>("RedeemedTime")
                        .HasColumnType("datetime(6)");

                    b.Property<ulong>("SteamID")
                        .HasColumnType("bigint unsigned");

                    b.HasKey("LogID");

                    b.HasIndex("CDKDataCKey");

                    b.HasIndex("CDKey", "SteamID")
                        .IsUnique();

                    b.ToTable("LogData");
                });

            modelBuilder.Entity("CDKPlugin.Entities.CDKData", b =>
                {
                    b.OwnsMany("CDKPlugin.Until.Wrapper.CDKItemWrapper", "Items", b1 =>
                        {
                            b1.Property<string>("CDKDataCKey")
                                .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            b1.Property<byte>("Amount")
                                .HasColumnType("tinyint unsigned");

                            b1.Property<byte>("Count")
                                .HasColumnType("tinyint unsigned");

                            b1.Property<ushort>("ItemID")
                                .HasColumnType("smallint unsigned");

                            b1.Property<byte>("Quality")
                                .HasColumnType("tinyint unsigned");

                            b1.Property<byte[]>("State")
                                .IsRequired()
                                .HasColumnType("longblob");

                            b1.HasKey("CDKDataCKey", "Id");

                            b1.ToTable("CDKItemWrapper");

                            b1.WithOwner()
                                .HasForeignKey("CDKDataCKey");
                        });
                });

            modelBuilder.Entity("CDKPlugin.Entities.LogData", b =>
                {
                    b.HasOne("CDKPlugin.Entities.CDKData", null)
                        .WithMany("LogDataList")
                        .HasForeignKey("CDKDataCKey");
                });
#pragma warning restore 612, 618
        }
    }
}
