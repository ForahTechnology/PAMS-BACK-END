﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PAMS.Persistence.Context;

namespace PAMS.Persistence.Migrations
{
    [DbContext(typeof(PAMSdbContext))]
    [Migration("20210912164511_Initial Migration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.5")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("PAMS.Domain.Entities.Client", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("RegisteredDate")
                        .HasColumnType("datetime2");

                    b.HasKey("ID");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("PAMS.Domain.Entities.ClientSample", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ClientID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("SampleTemplateID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ID");

                    b.ToTable("ClientSamples");
                });

            modelBuilder.Entity("PAMS.Domain.Entities.EquipmentInventory", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Condition")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Discountinued")
                        .HasColumnType("bit");

                    b.Property<string>("EquipmentName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IDTag")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Location")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ModelNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhotoLink")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Price")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PurchaseDate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Quantity")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SerialNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Supplier")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UnitPrice")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WarrantyExpiration")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("EquipmentInventories");
                });

            modelBuilder.Entity("PAMS.Domain.Entities.FMEnv", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("SectorName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("FMVEnvs");
                });

            modelBuilder.Entity("PAMS.Domain.Entities.FMEnvParameterTemplate", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("FMEnvID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Limit")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Parameter")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Unit")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("FMEnvID");

                    b.ToTable("FMEnvParameterTemplates");
                });

            modelBuilder.Entity("PAMS.Domain.Entities.Inventory", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateOfLastOrder")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ExpiryDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("ItemDiscontinued")
                        .HasColumnType("bit");

                    b.Property<string>("ItemName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ItemNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Location")
                        .HasColumnType("int");

                    b.Property<string>("Quantity")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("QuantityLeft")
                        .HasColumnType("decimal(18,6)");

                    b.Property<decimal>("QuantityUsed")
                        .HasColumnType("decimal(18,6)");

                    b.Property<string>("ReorderLevel")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StockQty")
                        .HasColumnType("int");

                    b.Property<decimal>("TotalValue")
                        .HasColumnType("decimal(18,6)");

                    b.Property<decimal>("UnitCost")
                        .HasColumnType("decimal(18,6)");

                    b.Property<string>("Weight")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Inventories");
                });

            modelBuilder.Entity("PAMS.Domain.Entities.Invoice", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ClientID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateGenerated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DueDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("InoviceNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Items")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Paid")
                        .HasColumnType("bit");

                    b.Property<Guid>("SamplingID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ID");

                    b.HasIndex("ClientID");

                    b.ToTable("Invoices");
                });

            modelBuilder.Entity("PAMS.Domain.Entities.MicroBiologicalAnalysis", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Limit")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Microbial_Group")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("ReportID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Result")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Test_Method")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Unit")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("ReportID");

                    b.ToTable("MicroBiologicalAnalyses");
                });

            modelBuilder.Entity("PAMS.Domain.Entities.MicroBiologicalAnalysisTemplate", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Limit")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Microbial_Group")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Result")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Test_Method")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Unit")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("MicroBiologicalAnalysisTemplates");
                });

            modelBuilder.Entity("PAMS.Domain.Entities.PamsUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("OTP")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("PasswordResetDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<DateTime>("RegisteredDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("PAMS.Domain.Entities.PhysicoChemicalAnalysis", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Limit")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("ReportID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Result")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Test_Method")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Test_Performed_And_Unit")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UC")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("ReportID");

                    b.ToTable("PhysicoChemicalAnalyses");
                });

            modelBuilder.Entity("PAMS.Domain.Entities.PhysicoChemicalAnalysisTemplate", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Limit")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Result")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Test_Method")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Test_Performed_And_Unit")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<string>("UC")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("PhysicoChemicalAnalysisTemplates");
                });

            modelBuilder.Entity("PAMS.Domain.Entities.PhysicoParameterTemplate", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Limit")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Parameter")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("PhysicoTemplateID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Unit")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("PhysicoTemplateID");

                    b.ToTable("PhysicoParameterTemplates");
                });

            modelBuilder.Entity("PAMS.Domain.Entities.PhysicoTemplate", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("PhysicoTemplates");
                });

            modelBuilder.Entity("PAMS.Domain.Entities.Report", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Batch_Number")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Certificate_Number")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("ClientID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Comment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Date_Analysed_In_Lab")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Date_Recieved_In_Lab")
                        .HasColumnType("datetime2");

                    b.Property<string>("Lab_Analyst")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Lab_Env_Con")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Lab_Sample_Ref_Number")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Sample_Label")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Sample_Type")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("SamplingID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ID");

                    b.ToTable("Reports");
                });

            modelBuilder.Entity("PAMS.Domain.Entities.Sample", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SampleType")
                        .HasColumnType("int");

                    b.Property<Guid>("SamplingID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ID");

                    b.HasIndex("SamplingID");

                    b.ToTable("Samples");
                });

            modelBuilder.Entity("PAMS.Domain.Entities.SampleTemplate", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SampleType")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("SampleTemplates");
                });

            modelBuilder.Entity("PAMS.Domain.Entities.Sampling", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ClientID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("GPSLocation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PicturePaths")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SampleType")
                        .HasColumnType("int");

                    b.Property<DateTime>("SamplingDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("SamplingTime")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("StaffId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("StaffName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("ClientID");

                    b.ToTable("Samplings");
                });

            modelBuilder.Entity("PAMS.Domain.Entities.Setting", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Lab_Director")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VAT")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Settings");
                });

            modelBuilder.Entity("PAMS.Domain.Entities.Test", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Limit")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("SampleID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("SampleID");

                    b.ToTable("Tests");
                });

            modelBuilder.Entity("PAMS.Domain.Entities.TestTemplate", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Limit")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("SampleTemplateID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("SampleTemplateID");

                    b.ToTable("TestTemplates");
                });

            modelBuilder.Entity("PAMS.Domain.Entities.UserActivation", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("UserActivations");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("PAMS.Domain.Entities.PamsUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("PAMS.Domain.Entities.PamsUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PAMS.Domain.Entities.PamsUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("PAMS.Domain.Entities.PamsUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PAMS.Domain.Entities.FMEnvParameterTemplate", b =>
                {
                    b.HasOne("PAMS.Domain.Entities.FMEnv", "FMEnv")
                        .WithMany("Parameters")
                        .HasForeignKey("FMEnvID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FMEnv");
                });

            modelBuilder.Entity("PAMS.Domain.Entities.Invoice", b =>
                {
                    b.HasOne("PAMS.Domain.Entities.Client", "Client")
                        .WithMany("Invoices")
                        .HasForeignKey("ClientID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");
                });

            modelBuilder.Entity("PAMS.Domain.Entities.MicroBiologicalAnalysis", b =>
                {
                    b.HasOne("PAMS.Domain.Entities.Report", null)
                        .WithMany("MicroBiologicalAnalyses")
                        .HasForeignKey("ReportID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PAMS.Domain.Entities.PhysicoChemicalAnalysis", b =>
                {
                    b.HasOne("PAMS.Domain.Entities.Report", null)
                        .WithMany("PhysicoChemicalAnalyses")
                        .HasForeignKey("ReportID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PAMS.Domain.Entities.PhysicoParameterTemplate", b =>
                {
                    b.HasOne("PAMS.Domain.Entities.PhysicoTemplate", "PhysicoTemplate")
                        .WithMany("AnalysisParameterTemplates")
                        .HasForeignKey("PhysicoTemplateID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PhysicoTemplate");
                });

            modelBuilder.Entity("PAMS.Domain.Entities.Sample", b =>
                {
                    b.HasOne("PAMS.Domain.Entities.Sampling", null)
                        .WithMany("Samples")
                        .HasForeignKey("SamplingID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PAMS.Domain.Entities.Sampling", b =>
                {
                    b.HasOne("PAMS.Domain.Entities.Client", "Client")
                        .WithMany("Samplings")
                        .HasForeignKey("ClientID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");
                });

            modelBuilder.Entity("PAMS.Domain.Entities.Test", b =>
                {
                    b.HasOne("PAMS.Domain.Entities.Sample", "Sample")
                        .WithMany("Tests")
                        .HasForeignKey("SampleID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Sample");
                });

            modelBuilder.Entity("PAMS.Domain.Entities.TestTemplate", b =>
                {
                    b.HasOne("PAMS.Domain.Entities.SampleTemplate", "SampleTemplate")
                        .WithMany("TestTemplates")
                        .HasForeignKey("SampleTemplateID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SampleTemplate");
                });

            modelBuilder.Entity("PAMS.Domain.Entities.Client", b =>
                {
                    b.Navigation("Invoices");

                    b.Navigation("Samplings");
                });

            modelBuilder.Entity("PAMS.Domain.Entities.FMEnv", b =>
                {
                    b.Navigation("Parameters");
                });

            modelBuilder.Entity("PAMS.Domain.Entities.PhysicoTemplate", b =>
                {
                    b.Navigation("AnalysisParameterTemplates");
                });

            modelBuilder.Entity("PAMS.Domain.Entities.Report", b =>
                {
                    b.Navigation("MicroBiologicalAnalyses");

                    b.Navigation("PhysicoChemicalAnalyses");
                });

            modelBuilder.Entity("PAMS.Domain.Entities.Sample", b =>
                {
                    b.Navigation("Tests");
                });

            modelBuilder.Entity("PAMS.Domain.Entities.SampleTemplate", b =>
                {
                    b.Navigation("TestTemplates");
                });

            modelBuilder.Entity("PAMS.Domain.Entities.Sampling", b =>
                {
                    b.Navigation("Samples");
                });
#pragma warning restore 612, 618
        }
    }
}
