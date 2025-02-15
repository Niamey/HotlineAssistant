﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Vostok.Hotline.Data.EF.DbStorage.Core;

namespace Vostok.Hotline.Data.EF.Migrations
{
    [DbContext(typeof(HotlineCoreContext))]
    [Migration("20210317142643_AddLoggingRequest")]
    partial class AddLoggingRequest
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Vostok.Hotline.Data.EF.Entities.Core.EnvironmentSetting", b =>
                {
                    b.Property<string>("EnvironmentKey")
                        .HasColumnName("environmentKey")
                        .HasColumnType("varchar(128)")
                        .HasMaxLength(128)
                        .IsUnicode(false);

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnName("createdOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("EnvironmentValue")
                        .HasColumnName("environmentValue")
                        .HasColumnType("varchar(max)")
                        .IsUnicode(false);

                    b.Property<Guid?>("SessionId")
                        .HasColumnName("sessionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("UpdatedOn")
                        .HasColumnName("updatedOn")
                        .HasColumnType("datetime2");

                    b.Property<byte[]>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnName("version")
                        .HasColumnType("rowversion");

                    b.HasKey("EnvironmentKey");

                    b.ToTable("EnvironmentSettings");
                });

            modelBuilder.Entity("Vostok.Hotline.Data.EF.Entities.Core.LoggingRequest", b =>
                {
                    b.Property<int>("LoggingRequestId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("loggingRequestId")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnName("createdOn")
                        .HasColumnType("datetime2");

                    b.Property<bool>("HasError")
                        .HasColumnName("hasError")
                        .HasColumnType("bit");

                    b.Property<int?>("LoggingOperationType")
                        .HasColumnName("loggingOperationType")
                        .HasColumnType("int");

                    b.Property<int>("LoggingSystemType")
                        .HasColumnName("loggingSystemType")
                        .HasColumnType("int");

                    b.Property<string>("RequestValue")
                        .HasColumnName("requestValue")
                        .HasColumnType("varchar(max)")
                        .IsUnicode(false);

                    b.Property<string>("ResponseValue")
                        .HasColumnName("responseValue")
                        .HasColumnType("varchar(max)")
                        .IsUnicode(false);

                    b.Property<Guid?>("SessionId")
                        .HasColumnName("sessionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("UniqueRequestId")
                        .HasColumnName("uniqueRequestId")
                        .HasColumnType("varchar(32)")
                        .HasMaxLength(32)
                        .IsUnicode(false);

                    b.Property<DateTime>("UpdatedOn")
                        .HasColumnName("updatedOn")
                        .HasColumnType("datetime2");

                    b.Property<byte[]>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnName("version")
                        .HasColumnType("rowversion");

                    b.HasKey("LoggingRequestId");

                    b.HasIndex("SessionId");

                    b.HasIndex("UniqueRequestId");

                    b.ToTable("LoggingRequests");
                });
#pragma warning restore 612, 618
        }
    }
}
