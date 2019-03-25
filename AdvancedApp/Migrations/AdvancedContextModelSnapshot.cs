﻿// <auto-generated />
using AdvancedApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AdvancedApp.Migrations
{
    [DbContext(typeof(AdvancedContext))]
    partial class AdvancedContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.8-servicing-32085")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AdvancedApp.Models.Employee", b =>
                {
                    b.Property<string>("SSN");

                    b.Property<string>("FirstName");

                    b.Property<string>("FamilyName");

                    b.Property<decimal>("Salary");

                    b.HasKey("SSN", "FirstName", "FamilyName");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("AdvancedApp.Models.SecondaryIdentity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("InActiveUse");

                    b.Property<string>("Name");

                    b.Property<string>("PrimaryFamilyName");

                    b.Property<string>("PrimaryFirstName");

                    b.Property<string>("PrimarySSN");

                    b.HasKey("Id");

                    b.HasIndex("PrimarySSN", "PrimaryFirstName", "PrimaryFamilyName")
                        .IsUnique()
                        .HasFilter("[PrimarySSN] IS NOT NULL AND [PrimaryFirstName] IS NOT NULL AND [PrimaryFamilyName] IS NOT NULL");

                    b.ToTable("SecondaryIdentity");
                });

            modelBuilder.Entity("AdvancedApp.Models.SecondaryIdentity", b =>
                {
                    b.HasOne("AdvancedApp.Models.Employee", "PrimaryIdentity")
                        .WithOne("OtherIdentity")
                        .HasForeignKey("AdvancedApp.Models.SecondaryIdentity", "PrimarySSN", "PrimaryFirstName", "PrimaryFamilyName");
                });
#pragma warning restore 612, 618
        }
    }
}
