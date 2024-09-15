﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NodesTask.Data;

#nullable disable

namespace NodesTask.Data.ApplicationDb
{
    [DbContext(typeof(NodesApplicationDbContext))]
    [Migration("20240915182336_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("NodesTask.Data.Entities.ExceptionJournal", b =>
                {
                    b.Property<Guid>("EventId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("BodyParams")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ExceptionType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("QueryParams")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StackTrace")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime2");

                    b.HasKey("EventId");

                    b.ToTable("ExceptionJournals");
                });

            modelBuilder.Entity("NodesTask.Data.Entities.Node", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("NodeName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("ParentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("TreeId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.HasIndex("TreeId");

                    b.ToTable("Nodes");
                });

            modelBuilder.Entity("NodesTask.Data.Entities.Tree", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("TreeName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Trees");
                });

            modelBuilder.Entity("NodesTask.Data.Entities.Node", b =>
                {
                    b.HasOne("NodesTask.Data.Entities.Node", "Parent")
                        .WithMany("Children")
                        .HasForeignKey("ParentId");

                    b.HasOne("NodesTask.Data.Entities.Tree", "Tree")
                        .WithMany("Nodes")
                        .HasForeignKey("TreeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Parent");

                    b.Navigation("Tree");
                });

            modelBuilder.Entity("NodesTask.Data.Entities.Node", b =>
                {
                    b.Navigation("Children");
                });

            modelBuilder.Entity("NodesTask.Data.Entities.Tree", b =>
                {
                    b.Navigation("Nodes");
                });
#pragma warning restore 612, 618
        }
    }
}
