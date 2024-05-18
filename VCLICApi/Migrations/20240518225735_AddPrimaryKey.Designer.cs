﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using VCLICApi.Data;

#nullable disable

namespace VCLICApi.Migrations
{
    [DbContext(typeof(MyDbContext))]
    [Migration("20240518225735_AddPrimaryKey")]
    partial class AddPrimaryKey
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("ValueSet", b =>
                {
                    b.Property<int>("ValueSetId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Medications")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("ValueSetName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.HasKey("ValueSetId");

                    b.ToTable("ValueSets");
                });

            modelBuilder.Entity("VCLICApi.Model.BetaBlockerValueSet", b =>
                {
                    b.Property<string>("ValueSetId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Medications")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("ValueSetName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("ValueSetId");

                    b.ToTable("BetaBlockerValueSets");
                });

            modelBuilder.Entity("VCLICApi.Model.Medication", b =>
                {
                    b.Property<int>("MedicationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Inpatients")
                        .HasColumnType("int");

                    b.Property<string>("MedName")
                        .HasColumnType("longtext");

                    b.Property<int>("Outpatients")
                        .HasColumnType("int");

                    b.Property<int>("Patients")
                        .HasColumnType("int");

                    b.Property<string>("Route")
                        .HasColumnType("longtext");

                    b.Property<string>("SimpleGenericName")
                        .HasColumnType("longtext");

                    b.HasKey("MedicationId");

                    b.ToTable("Medications");
                });
#pragma warning restore 612, 618
        }
    }
}
