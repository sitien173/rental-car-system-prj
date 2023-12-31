﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NGOT.Infrastructure.Data;

#nullable disable

namespace NGOT.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230907152336_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("NGOT.ApplicationCore.Entities.AdditionalFees", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.Property<string>("Unit")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("AdditionalFees");
                });

            modelBuilder.Entity("NGOT.ApplicationCore.Entities.Brand", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Brands");
                });

            modelBuilder.Entity("NGOT.ApplicationCore.Entities.Car", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BrandId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CarTypeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .HasColumnType("nvarchar(2000)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<decimal>("Price")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.Property<string>("Rule")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BrandId");

                    b.HasIndex("CarTypeId");

                    b.ToTable("Cars");
                });

            modelBuilder.Entity("NGOT.ApplicationCore.Entities.CarAdditionalFees", b =>
                {
                    b.Property<Guid>("CarId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AdditionalFeesId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("CarId", "AdditionalFeesId");

                    b.HasIndex("AdditionalFeesId");

                    b.ToTable("CarAdditionalFees");
                });

            modelBuilder.Entity("NGOT.ApplicationCore.Entities.CarFeature", b =>
                {
                    b.Property<Guid>("CarId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("FeatureId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("CarId", "FeatureId");

                    b.HasIndex("FeatureId");

                    b.ToTable("CarFeatures");
                });

            modelBuilder.Entity("NGOT.ApplicationCore.Entities.CarImage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CarId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsMain")
                        .HasColumnType("bit");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.HasKey("Id");

                    b.HasIndex("CarId");

                    b.ToTable("CarImages");
                });

            modelBuilder.Entity("NGOT.ApplicationCore.Entities.CarRentalDocuments", b =>
                {
                    b.Property<Guid>("CarId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RentalDocumentId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("CarId", "RentalDocumentId");

                    b.HasIndex("RentalDocumentId");

                    b.ToTable("CarRentalDocuments");
                });

            modelBuilder.Entity("NGOT.ApplicationCore.Entities.CarType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("CarTypes");
                });

            modelBuilder.Entity("NGOT.ApplicationCore.Entities.CheckListItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Comment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("VehicleHandoverId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("VehicleHandoverId");

                    b.ToTable("CheckListItems");
                });

            modelBuilder.Entity("NGOT.ApplicationCore.Entities.DamageAssessment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("AssessmentDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("DamageDescription")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("RentalContractId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<decimal>("RepairCost")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.Property<decimal>("TotalCost")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("RentalContractId");

                    b.ToTable("DamageAssessments");
                });

            modelBuilder.Entity("NGOT.ApplicationCore.Entities.Feature", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Features");
                });

            modelBuilder.Entity("NGOT.ApplicationCore.Entities.Payment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Amount")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("PaymentDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("PaymentMethod")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PaymentType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RentalContractId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("RentalContractId");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("NGOT.ApplicationCore.Entities.RentalContract", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime?>("AccidentDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("AccidentDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Amount")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime?>("CancellationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("CancellationReason")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("LateReason")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("PrepaidAmount")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("RentalRequestId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("RentalRequestId")
                        .IsUnique();

                    b.ToTable("RentalContracts");
                });

            modelBuilder.Entity("NGOT.ApplicationCore.Entities.RentalDocuments", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("RentalDocuments");
                });

            modelBuilder.Entity("NGOT.ApplicationCore.Entities.RentalRequest", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<Guid>("CarId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("RentalContractId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("CarId");

                    b.ToTable("RentalRequests");
                });

            modelBuilder.Entity("NGOT.ApplicationCore.Entities.VehicleHandover", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("HandoverBy")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("HandoverDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("HandoverType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RentalContractId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.HasKey("Id");

                    b.HasIndex("RentalContractId");

                    b.ToTable("VehicleHandovers");
                });

            modelBuilder.Entity("NGOT.ApplicationCore.Entities.Car", b =>
                {
                    b.HasOne("NGOT.ApplicationCore.Entities.Brand", "Brand")
                        .WithMany("Cars")
                        .HasForeignKey("BrandId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("NGOT.ApplicationCore.Entities.CarType", "CarType")
                        .WithMany("Cars")
                        .HasForeignKey("CarTypeId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.OwnsOne("NGOT.ApplicationCore.ValueObjects.CarSpecificity", "Specificity", b1 =>
                        {
                            b1.Property<Guid>("CarId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Fuel")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("FuelConsumption")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<int>("Seat")
                                .HasColumnType("int");

                            b1.Property<string>("Transmission")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("CarId");

                            b1.ToTable("Cars");

                            b1.WithOwner()
                                .HasForeignKey("CarId");
                        });

                    b.Navigation("Brand");

                    b.Navigation("CarType");

                    b.Navigation("Specificity")
                        .IsRequired();
                });

            modelBuilder.Entity("NGOT.ApplicationCore.Entities.CarAdditionalFees", b =>
                {
                    b.HasOne("NGOT.ApplicationCore.Entities.AdditionalFees", "AdditionalFees")
                        .WithMany("CarAdditionalFees")
                        .HasForeignKey("AdditionalFeesId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("NGOT.ApplicationCore.Entities.Car", "Car")
                        .WithMany("CarAdditionalFees")
                        .HasForeignKey("CarId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AdditionalFees");

                    b.Navigation("Car");
                });

            modelBuilder.Entity("NGOT.ApplicationCore.Entities.CarFeature", b =>
                {
                    b.HasOne("NGOT.ApplicationCore.Entities.Car", "Car")
                        .WithMany("CarFeatures")
                        .HasForeignKey("CarId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NGOT.ApplicationCore.Entities.Feature", "Feature")
                        .WithMany("CarFeatures")
                        .HasForeignKey("FeatureId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Car");

                    b.Navigation("Feature");
                });

            modelBuilder.Entity("NGOT.ApplicationCore.Entities.CarImage", b =>
                {
                    b.HasOne("NGOT.ApplicationCore.Entities.Car", "Car")
                        .WithMany("CarImages")
                        .HasForeignKey("CarId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("NGOT.ApplicationCore.ValueObjects.Image", "Image", b1 =>
                        {
                            b1.Property<Guid>("CarImageId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("FileName")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Host")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<int>("Size")
                                .HasColumnType("int");

                            b1.Property<string>("Thumbnail")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Type")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("CarImageId");

                            b1.ToTable("CarImages");

                            b1.WithOwner()
                                .HasForeignKey("CarImageId");
                        });

                    b.Navigation("Car");

                    b.Navigation("Image")
                        .IsRequired();
                });

            modelBuilder.Entity("NGOT.ApplicationCore.Entities.CarRentalDocuments", b =>
                {
                    b.HasOne("NGOT.ApplicationCore.Entities.Car", "Car")
                        .WithMany("CarRentalDocuments")
                        .HasForeignKey("CarId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NGOT.ApplicationCore.Entities.RentalDocuments", "RentalDocuments")
                        .WithMany("CarRentalDocuments")
                        .HasForeignKey("RentalDocumentId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Car");

                    b.Navigation("RentalDocuments");
                });

            modelBuilder.Entity("NGOT.ApplicationCore.Entities.CarType", b =>
                {
                    b.OwnsOne("NGOT.ApplicationCore.ValueObjects.Icon", "Icon", b1 =>
                        {
                            b1.Property<Guid>("CarTypeId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("FileName")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Host")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<int>("Size")
                                .HasColumnType("int");

                            b1.Property<string>("Type")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("CarTypeId");

                            b1.ToTable("CarTypes");

                            b1.WithOwner()
                                .HasForeignKey("CarTypeId");
                        });

                    b.Navigation("Icon")
                        .IsRequired();
                });

            modelBuilder.Entity("NGOT.ApplicationCore.Entities.CheckListItem", b =>
                {
                    b.HasOne("NGOT.ApplicationCore.Entities.VehicleHandover", "VehicleHandover")
                        .WithMany("CheckListItems")
                        .HasForeignKey("VehicleHandoverId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("VehicleHandover");
                });

            modelBuilder.Entity("NGOT.ApplicationCore.Entities.DamageAssessment", b =>
                {
                    b.HasOne("NGOT.ApplicationCore.Entities.RentalContract", "RentalContract")
                        .WithMany("DamageAssessments")
                        .HasForeignKey("RentalContractId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("RentalContract");
                });

            modelBuilder.Entity("NGOT.ApplicationCore.Entities.Feature", b =>
                {
                    b.OwnsOne("NGOT.ApplicationCore.ValueObjects.Icon", "Icon", b1 =>
                        {
                            b1.Property<Guid>("FeatureId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("FileName")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Host")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<int>("Size")
                                .HasColumnType("int");

                            b1.Property<string>("Type")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("FeatureId");

                            b1.ToTable("Features");

                            b1.WithOwner()
                                .HasForeignKey("FeatureId");
                        });

                    b.Navigation("Icon")
                        .IsRequired();
                });

            modelBuilder.Entity("NGOT.ApplicationCore.Entities.Payment", b =>
                {
                    b.HasOne("NGOT.ApplicationCore.Entities.RentalContract", "RentalContract")
                        .WithMany("Payments")
                        .HasForeignKey("RentalContractId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("RentalContract");
                });

            modelBuilder.Entity("NGOT.ApplicationCore.Entities.RentalContract", b =>
                {
                    b.HasOne("NGOT.ApplicationCore.Entities.RentalRequest", "RentalRequest")
                        .WithOne("RentalContract")
                        .HasForeignKey("NGOT.ApplicationCore.Entities.RentalContract", "RentalRequestId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("RentalRequest");
                });

            modelBuilder.Entity("NGOT.ApplicationCore.Entities.RentalRequest", b =>
                {
                    b.HasOne("NGOT.ApplicationCore.Entities.Car", "Car")
                        .WithMany("RentalRequests")
                        .HasForeignKey("CarId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Car");
                });

            modelBuilder.Entity("NGOT.ApplicationCore.Entities.VehicleHandover", b =>
                {
                    b.HasOne("NGOT.ApplicationCore.Entities.RentalContract", "RentalContract")
                        .WithMany("VehicleHandovers")
                        .HasForeignKey("RentalContractId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("RentalContract");
                });

            modelBuilder.Entity("NGOT.ApplicationCore.Entities.AdditionalFees", b =>
                {
                    b.Navigation("CarAdditionalFees");
                });

            modelBuilder.Entity("NGOT.ApplicationCore.Entities.Brand", b =>
                {
                    b.Navigation("Cars");
                });

            modelBuilder.Entity("NGOT.ApplicationCore.Entities.Car", b =>
                {
                    b.Navigation("CarAdditionalFees");

                    b.Navigation("CarFeatures");

                    b.Navigation("CarImages");

                    b.Navigation("CarRentalDocuments");

                    b.Navigation("RentalRequests");
                });

            modelBuilder.Entity("NGOT.ApplicationCore.Entities.CarType", b =>
                {
                    b.Navigation("Cars");
                });

            modelBuilder.Entity("NGOT.ApplicationCore.Entities.Feature", b =>
                {
                    b.Navigation("CarFeatures");
                });

            modelBuilder.Entity("NGOT.ApplicationCore.Entities.RentalContract", b =>
                {
                    b.Navigation("DamageAssessments");

                    b.Navigation("Payments");

                    b.Navigation("VehicleHandovers");
                });

            modelBuilder.Entity("NGOT.ApplicationCore.Entities.RentalDocuments", b =>
                {
                    b.Navigation("CarRentalDocuments");
                });

            modelBuilder.Entity("NGOT.ApplicationCore.Entities.RentalRequest", b =>
                {
                    b.Navigation("RentalContract");
                });

            modelBuilder.Entity("NGOT.ApplicationCore.Entities.VehicleHandover", b =>
                {
                    b.Navigation("CheckListItems");
                });
#pragma warning restore 612, 618
        }
    }
}
