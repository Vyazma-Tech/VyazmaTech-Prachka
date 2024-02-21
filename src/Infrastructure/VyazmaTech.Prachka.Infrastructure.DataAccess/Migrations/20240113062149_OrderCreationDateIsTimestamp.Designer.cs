﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using VyazmaTech.Prachka.Infrastructure.DataAccess.Contexts;

#nullable disable

namespace VyazmaTech.Prachka.Infrastructure.DataAccess.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20240113062149_OrderCreationDateIsTimestamp")]
    partial class OrderCreationDateIsTimestamp
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("VyazmaTech.Prachka.Infrastructure.DataAccess.Models.OrderModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("Paid")
                        .HasColumnType("boolean");

                    b.Property<Guid>("QueueId")
                        .HasColumnType("uuid");

                    b.Property<bool>("Ready")
                        .HasColumnType("boolean");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("QueueId");

                    b.HasIndex("UserId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("VyazmaTech.Prachka.Infrastructure.DataAccess.Models.OrderSubscriptionModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateOnly>("CreationDate")
                        .HasColumnType("date");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("OrderSubscriptions");
                });

            modelBuilder.Entity("VyazmaTech.Prachka.Infrastructure.DataAccess.Models.QueueModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<TimeOnly>("ActiveFrom")
                        .HasColumnType("time without time zone");

                    b.Property<TimeOnly>("ActiveUntil")
                        .HasColumnType("time without time zone");

                    b.Property<DateOnly>("AssignmentDate")
                        .HasColumnType("date");

                    b.Property<int>("Capacity")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Queues");
                });

            modelBuilder.Entity("VyazmaTech.Prachka.Infrastructure.DataAccess.Models.QueueSubscriptionModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateOnly>("CreationDate")
                        .HasColumnType("date");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("QueueSubscriptions");
                });

            modelBuilder.Entity("VyazmaTech.Prachka.Infrastructure.DataAccess.Models.UserModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Fullname")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("OrderSubscriptionId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("QueueSubscriptionId")
                        .HasColumnType("uuid");

                    b.Property<DateOnly>("RegistrationDate")
                        .HasColumnType("date");

                    b.Property<string>("TelegramId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("OrderModelOrderSubscriptionModel", b =>
                {
                    b.Property<Guid>("OrderSubscriptionModelId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("OrdersId")
                        .HasColumnType("uuid");

                    b.HasKey("OrderSubscriptionModelId", "OrdersId");

                    b.HasIndex("OrdersId");

                    b.ToTable("UserOrdersAndTheirSubscriptions", (string)null);
                });

            modelBuilder.Entity("QueueModelQueueSubscriptionModel", b =>
                {
                    b.Property<Guid>("QueueSubscriptionModelId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("QueuesId")
                        .HasColumnType("uuid");

                    b.HasKey("QueueSubscriptionModelId", "QueuesId");

                    b.HasIndex("QueuesId");

                    b.ToTable("UserQueuesAndTheirSubscriptions", (string)null);
                });

            modelBuilder.Entity("VyazmaTech.Prachka.Infrastructure.DataAccess.Models.OrderModel", b =>
                {
                    b.HasOne("VyazmaTech.Prachka.Infrastructure.DataAccess.Models.QueueModel", "Queue")
                        .WithMany("Orders")
                        .HasForeignKey("QueueId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VyazmaTech.Prachka.Infrastructure.DataAccess.Models.UserModel", "User")
                        .WithMany("Orders")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Queue");

                    b.Navigation("User");
                });

            modelBuilder.Entity("VyazmaTech.Prachka.Infrastructure.DataAccess.Models.OrderSubscriptionModel", b =>
                {
                    b.HasOne("VyazmaTech.Prachka.Infrastructure.DataAccess.Models.UserModel", "User")
                        .WithOne("OrderSubscription")
                        .HasForeignKey("VyazmaTech.Prachka.Infrastructure.DataAccess.Models.OrderSubscriptionModel", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("VyazmaTech.Prachka.Infrastructure.DataAccess.Models.QueueSubscriptionModel", b =>
                {
                    b.HasOne("VyazmaTech.Prachka.Infrastructure.DataAccess.Models.UserModel", "User")
                        .WithOne("QueueSubscription")
                        .HasForeignKey("VyazmaTech.Prachka.Infrastructure.DataAccess.Models.QueueSubscriptionModel", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("OrderModelOrderSubscriptionModel", b =>
                {
                    b.HasOne("VyazmaTech.Prachka.Infrastructure.DataAccess.Models.OrderSubscriptionModel", null)
                        .WithMany()
                        .HasForeignKey("OrderSubscriptionModelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VyazmaTech.Prachka.Infrastructure.DataAccess.Models.OrderModel", null)
                        .WithMany()
                        .HasForeignKey("OrdersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("QueueModelQueueSubscriptionModel", b =>
                {
                    b.HasOne("VyazmaTech.Prachka.Infrastructure.DataAccess.Models.QueueSubscriptionModel", null)
                        .WithMany()
                        .HasForeignKey("QueueSubscriptionModelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VyazmaTech.Prachka.Infrastructure.DataAccess.Models.QueueModel", null)
                        .WithMany()
                        .HasForeignKey("QueuesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("VyazmaTech.Prachka.Infrastructure.DataAccess.Models.QueueModel", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("VyazmaTech.Prachka.Infrastructure.DataAccess.Models.UserModel", b =>
                {
                    b.Navigation("OrderSubscription")
                        .IsRequired();

                    b.Navigation("Orders");

                    b.Navigation("QueueSubscription")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}