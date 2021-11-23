﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TTGS.Infrastructure.EF;

namespace TTGS.Infrastructure.EF.Data.Migrations.Main
{
    [DbContext(typeof(TTGSContext))]
    partial class TTGSContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("TTGS.Shared.Entity.Communication", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsCreatedByAdmin")
                        .HasColumnType("bit");

                    b.Property<string>("Message")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RecipientId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Subject")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("Communications");
                });

            modelBuilder.Entity("TTGS.Shared.Entity.Order", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<string>("Commodity")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<string>("DropOffPoints")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Exporter")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImportBorder")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Importer")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NumberOfTruck")
                        .HasColumnType("int");

                    b.Property<string>("OrderStatus")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OwnerId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PickupPoints")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Quantity")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TransitBorders")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("TTGS.Shared.Entity.Order", b =>
                {
                    b.OwnsMany("TTGS.Shared.Entity.OrderAddress", "Addresses", b1 =>
                        {
                            b1.Property<Guid>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Address")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("AddressType")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("ContactPerson")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("EmailAddress")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<long>("OrderId")
                                .HasColumnType("bigint");

                            b1.Property<string>("PhoneNumbers")
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("Id");

                            b1.HasIndex("OrderId");

                            b1.ToTable("OrderAddress");

                            b1.WithOwner()
                                .HasForeignKey("OrderId");
                        });

                    b.Navigation("Addresses");
                });
#pragma warning restore 612, 618
        }
    }
}
