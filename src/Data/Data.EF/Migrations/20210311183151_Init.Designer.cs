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
    [Migration("20210311183151_Init")]
    partial class Init
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
#pragma warning restore 612, 618
        }
    }
}
