﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Publicator.Infrastructure;

namespace Publicator.Infrastructure.Migrations
{
    [DbContext(typeof(PublicatorDbContext))]
    [Migration("20200121211800_Bugfix")]
    partial class Bugfix
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Publicator.Infrastructure.Entities.Bookmark", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("smalldatetime");

                    b.Property<Guid>("PostId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.HasIndex("UserId");

                    b.ToTable("Bookmarks");

                    b.HasData(
                        new
                        {
                            Id = new Guid("ef49b3bb-1c77-4347-9a12-744e0505d559"),
                            CreationDate = new DateTime(2020, 1, 24, 23, 17, 59, 246, DateTimeKind.Local).AddTicks(5327),
                            PostId = new Guid("9b9fe4fc-b300-4651-85ca-d92c4d599e18"),
                            UserId = new Guid("3de9d387-4cf9-44e4-a0af-0395f4531c86")
                        });
                });

            modelBuilder.Entity("Publicator.Infrastructure.Entities.Comment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)")
                        .IsUnicode(true);

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("smalldatetime");

                    b.Property<Guid?>("ParentRepliedCommentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PostId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ParentRepliedCommentId");

                    b.HasIndex("PostId");

                    b.HasIndex("UserId");

                    b.ToTable("Comments");

                    b.HasData(
                        new
                        {
                            Id = new Guid("67d1b6d8-e005-4573-b6c2-c64b29df652f"),
                            Content = "<p>Hilarious, how it happened with you</p>",
                            CreationDate = new DateTime(2020, 1, 22, 23, 17, 59, 246, DateTimeKind.Local).AddTicks(2469),
                            PostId = new Guid("80b5abde-0ddd-4331-8fcd-44a93e3ddcd6"),
                            UserId = new Guid("3de9d387-4cf9-44e4-a0af-0395f4531c86")
                        },
                        new
                        {
                            Id = new Guid("9c5c6d31-ca66-48a0-abd4-1086f900ed78"),
                            Content = "<p>It happened in 2016 year, i was 16 years old</p>",
                            CreationDate = new DateTime(2020, 1, 23, 23, 17, 59, 246, DateTimeKind.Local).AddTicks(3915),
                            ParentRepliedCommentId = new Guid("67d1b6d8-e005-4573-b6c2-c64b29df652f"),
                            PostId = new Guid("80b5abde-0ddd-4331-8fcd-44a93e3ddcd6"),
                            UserId = new Guid("011438e9-5080-451f-9149-559d658b7b25")
                        });
                });

            modelBuilder.Entity("Publicator.Infrastructure.Entities.Community", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("CreatorUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(64)")
                        .HasMaxLength(64)
                        .IsUnicode(true);

                    b.Property<string>("PictureName")
                        .HasColumnType("nvarchar(32)")
                        .HasMaxLength(32);

                    b.HasKey("Id");

                    b.HasIndex("CreatorUserId");

                    b.ToTable("Communities");

                    b.HasData(
                        new
                        {
                            Id = new Guid("34e68237-4a11-41ef-97fc-717ae3d13e6c"),
                            CreationDate = new DateTime(2020, 1, 21, 23, 17, 59, 245, DateTimeKind.Local).AddTicks(2581),
                            CreatorUserId = new Guid("3de9d387-4cf9-44e4-a0af-0395f4531c86"),
                            Description = "Here users post stories about theirs lifes.",
                            Name = "Stories about Life"
                        },
                        new
                        {
                            Id = new Guid("2b828160-0315-4341-aaa3-a971b72c2139"),
                            CreationDate = new DateTime(2020, 1, 21, 23, 17, 59, 245, DateTimeKind.Local).AddTicks(4034),
                            CreatorUserId = new Guid("3de9d387-4cf9-44e4-a0af-0395f4531c86"),
                            Description = "Community about vehicles and all about it.",
                            Name = "Vehicle"
                        });
                });

            modelBuilder.Entity("Publicator.Infrastructure.Entities.Post", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CommunityId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Content")
                        .HasColumnType("ntext");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("smalldatetime");

                    b.Property<Guid>("CreatorUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("CurrentRating")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128)
                        .IsUnicode(true);

                    b.HasKey("Id");

                    b.HasIndex("CommunityId");

                    b.HasIndex("CreatorUserId");

                    b.ToTable("Posts");

                    b.HasData(
                        new
                        {
                            Id = new Guid("80b5abde-0ddd-4331-8fcd-44a93e3ddcd6"),
                            CommunityId = new Guid("34e68237-4a11-41ef-97fc-717ae3d13e6c"),
                            Content = "<p>In this post i want to tell you story of my life...</p>",
                            CreationDate = new DateTime(2020, 1, 21, 23, 17, 59, 245, DateTimeKind.Local).AddTicks(6710),
                            CreatorUserId = new Guid("3de9d387-4cf9-44e4-a0af-0395f4531c86"),
                            CurrentRating = 0,
                            Name = "Post about my life"
                        },
                        new
                        {
                            Id = new Guid("9b9fe4fc-b300-4651-85ca-d92c4d599e18"),
                            CommunityId = new Guid("34e68237-4a11-41ef-97fc-717ae3d13e6c"),
                            Content = "<p>Once upon a time in Ukraine, village Sokal i studied in school...</p>",
                            CreationDate = new DateTime(2020, 1, 21, 23, 17, 59, 246, DateTimeKind.Local).AddTicks(280),
                            CreatorUserId = new Guid("011438e9-5080-451f-9149-559d658b7b25"),
                            CurrentRating = 0,
                            Name = "Happened in Sokal"
                        });
                });

            modelBuilder.Entity("Publicator.Infrastructure.Entities.PostTag", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PostId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("TagId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.HasIndex("TagId");

                    b.ToTable("PostTags");

                    b.HasData(
                        new
                        {
                            Id = new Guid("869170b6-3a1a-471a-8059-9a3a49672821"),
                            PostId = new Guid("80b5abde-0ddd-4331-8fcd-44a93e3ddcd6"),
                            TagId = new Guid("eab2c81c-cd7e-4f7d-ba79-686a3dd8a61e")
                        },
                        new
                        {
                            Id = new Guid("fad7973a-7b3a-4fd2-ba91-029e16818ff8"),
                            PostId = new Guid("80b5abde-0ddd-4331-8fcd-44a93e3ddcd6"),
                            TagId = new Guid("d6ab3a80-9321-46a9-829d-388e0a6c47d5")
                        });
                });

            modelBuilder.Entity("Publicator.Infrastructure.Entities.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(32)")
                        .HasMaxLength(32);

                    b.HasKey("Id");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = new Guid("ceec90a5-821f-485d-a812-847be50260b4"),
                            Name = "Administrator"
                        },
                        new
                        {
                            Id = new Guid("282db597-0df6-4f49-9832-3dedd27ee818"),
                            Name = "Moderator"
                        },
                        new
                        {
                            Id = new Guid("88d99ed8-8e71-4123-b235-d179db86ccdd"),
                            Name = "Simple"
                        });
                });

            modelBuilder.Entity("Publicator.Infrastructure.Entities.State", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(32)")
                        .HasMaxLength(32);

                    b.HasKey("Id");

                    b.ToTable("States");

                    b.HasData(
                        new
                        {
                            Id = new Guid("d9eafe03-b6cb-4bf8-bc19-197621eed68e"),
                            Name = "Active"
                        },
                        new
                        {
                            Id = new Guid("32cf6a4d-241e-4138-9927-7cee554024fe"),
                            Name = "Freezed"
                        });
                });

            modelBuilder.Entity("Publicator.Infrastructure.Entities.SubscriptionNewPost", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PostId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("SubscriptionCommunityId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("SubscriptionTagId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("SubscriptionUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.HasIndex("SubscriptionCommunityId");

                    b.HasIndex("SubscriptionTagId");

                    b.HasIndex("SubscriptionUserId");

                    b.HasIndex("UserId");

                    b.ToTable("SubscriptionNewPosts");
                });

            modelBuilder.Entity("Publicator.Infrastructure.Entities.Tag", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)")
                        .IsUnicode(true);

                    b.HasKey("Id");

                    b.ToTable("Tags");

                    b.HasData(
                        new
                        {
                            Id = new Guid("eab2c81c-cd7e-4f7d-ba79-686a3dd8a61e"),
                            Name = "Life"
                        },
                        new
                        {
                            Id = new Guid("f5625976-84c0-4ece-ae08-fdf975417951"),
                            Name = "Motorcycle"
                        },
                        new
                        {
                            Id = new Guid("5d6de9ac-6c8e-4c15-b0bb-bd9020c7f325"),
                            Name = "Car"
                        },
                        new
                        {
                            Id = new Guid("d6ab3a80-9321-46a9-829d-388e0a6c47d5"),
                            Name = "Politic"
                        });
                });

            modelBuilder.Entity("Publicator.Infrastructure.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("BeginStateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128)
                        .IsUnicode(true);

                    b.Property<bool>("EmailConfirmed")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<DateTime>("EndStateDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("JoinDate")
                        .HasColumnType("smalldatetime");

                    b.Property<string>("Nickname")
                        .HasColumnType("nvarchar(64)")
                        .HasMaxLength(64)
                        .IsUnicode(true);

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(64)")
                        .HasMaxLength(64)
                        .IsUnicode(true);

                    b.Property<string>("PictureName")
                        .HasColumnType("nvarchar(64)")
                        .HasMaxLength(64)
                        .IsUnicode(true);

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("StateId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("StateId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = new Guid("3de9d387-4cf9-44e4-a0af-0395f4531c86"),
                            BeginStateDate = new DateTime(2020, 1, 21, 23, 17, 59, 244, DateTimeKind.Local).AddTicks(6066),
                            Email = "lineyk27gg@gmail.com",
                            EmailConfirmed = false,
                            EndStateDate = new DateTime(2050, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            JoinDate = new DateTime(2020, 1, 21, 23, 17, 59, 239, DateTimeKind.Local).AddTicks(4199),
                            Nickname = "lineyk27gg",
                            PasswordHash = "ef797c8118f02dfb649607dd5d3f8c7623048c9c063d532cc95c5ed7a898a64f",
                            RoleId = new Guid("88d99ed8-8e71-4123-b235-d179db86ccdd"),
                            StateId = new Guid("d9eafe03-b6cb-4bf8-bc19-197621eed68e")
                        },
                        new
                        {
                            Id = new Guid("011438e9-5080-451f-9149-559d658b7b25"),
                            BeginStateDate = new DateTime(2020, 1, 21, 23, 17, 59, 245, DateTimeKind.Local).AddTicks(510),
                            Email = "lineyk27@yandex.ru",
                            EmailConfirmed = false,
                            EndStateDate = new DateTime(2050, 12, 13, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            JoinDate = new DateTime(2020, 1, 21, 23, 17, 59, 245, DateTimeKind.Local).AddTicks(475),
                            Nickname = "lineyk27",
                            PasswordHash = "5e884898da28047151d0e56f8dc6292773603d0d6aabbdd62a11ef721d1542d8",
                            RoleId = new Guid("88d99ed8-8e71-4123-b235-d179db86ccdd"),
                            StateId = new Guid("d9eafe03-b6cb-4bf8-bc19-197621eed68e")
                        });
                });

            modelBuilder.Entity("Publicator.Infrastructure.Entities.UserCommunity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CommunityId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("smalldatetime");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("CommunityId");

                    b.HasIndex("UserId");

                    b.ToTable("UserCommunities");
                });

            modelBuilder.Entity("Publicator.Infrastructure.Entities.UserSubscription", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("SubscriberUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("SubscriptionUserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("SubscriberUserId");

                    b.HasIndex("SubscriptionUserId");

                    b.ToTable("UserSubscriptions");

                    b.HasData(
                        new
                        {
                            Id = new Guid("8edeb646-d687-4b84-acf7-836c54a0befd"),
                            SubscriberUserId = new Guid("3de9d387-4cf9-44e4-a0af-0395f4531c86"),
                            SubscriptionUserId = new Guid("011438e9-5080-451f-9149-559d658b7b25")
                        });
                });

            modelBuilder.Entity("Publicator.Infrastructure.Entities.UserTag", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("TagId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("TagId");

                    b.HasIndex("UserId");

                    b.ToTable("UserTags");
                });

            modelBuilder.Entity("Publicator.Infrastructure.Entities.Vote", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("smalldatetime");

                    b.Property<Guid>("PostId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Up")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(true);

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.HasIndex("UserId");

                    b.ToTable("Votes");
                });

            modelBuilder.Entity("Publicator.Infrastructure.Entities.Bookmark", b =>
                {
                    b.HasOne("Publicator.Infrastructure.Entities.Post", "Post")
                        .WithMany("Bookmarks")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Publicator.Infrastructure.Entities.User", "User")
                        .WithMany("Bookmarks")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction);
                });

            modelBuilder.Entity("Publicator.Infrastructure.Entities.Comment", b =>
                {
                    b.HasOne("Publicator.Infrastructure.Entities.Comment", "ParentRepliedComment")
                        .WithMany("RepliesComments")
                        .HasForeignKey("ParentRepliedCommentId");

                    b.HasOne("Publicator.Infrastructure.Entities.Post", "Post")
                        .WithMany("Comments")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Publicator.Infrastructure.Entities.User", "User")
                        .WithMany("Comments")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("Publicator.Infrastructure.Entities.Community", b =>
                {
                    b.HasOne("Publicator.Infrastructure.Entities.User", "CreatorUser")
                        .WithMany("CreatedCommunities")
                        .HasForeignKey("CreatorUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Publicator.Infrastructure.Entities.Post", b =>
                {
                    b.HasOne("Publicator.Infrastructure.Entities.Community", "Community")
                        .WithMany("Posts")
                        .HasForeignKey("CommunityId");

                    b.HasOne("Publicator.Infrastructure.Entities.User", "CreatorUser")
                        .WithMany("Posts")
                        .HasForeignKey("CreatorUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Publicator.Infrastructure.Entities.PostTag", b =>
                {
                    b.HasOne("Publicator.Infrastructure.Entities.Post", "Post")
                        .WithMany("PostTags")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Publicator.Infrastructure.Entities.Tag", "Tag")
                        .WithMany("PostTags")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Publicator.Infrastructure.Entities.SubscriptionNewPost", b =>
                {
                    b.HasOne("Publicator.Infrastructure.Entities.Post", "Post")
                        .WithMany("SubscriptionNewPosts")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Publicator.Infrastructure.Entities.Community", "SubscriptionCommunity")
                        .WithMany("SubscriptionNewPosts")
                        .HasForeignKey("SubscriptionCommunityId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Publicator.Infrastructure.Entities.Tag", "SubscriptionTag")
                        .WithMany()
                        .HasForeignKey("SubscriptionTagId");

                    b.HasOne("Publicator.Infrastructure.Entities.User", "SubscriptionUser")
                        .WithMany()
                        .HasForeignKey("SubscriptionUserId");

                    b.HasOne("Publicator.Infrastructure.Entities.User", "User")
                        .WithMany("SubscriptionNewPosts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("Publicator.Infrastructure.Entities.User", b =>
                {
                    b.HasOne("Publicator.Infrastructure.Entities.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Publicator.Infrastructure.Entities.State", "State")
                        .WithMany("Users")
                        .HasForeignKey("StateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Publicator.Infrastructure.Entities.UserCommunity", b =>
                {
                    b.HasOne("Publicator.Infrastructure.Entities.Community", "Community")
                        .WithMany("UserCommunities")
                        .HasForeignKey("CommunityId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Publicator.Infrastructure.Entities.User", "User")
                        .WithMany("UserCommunities")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("Publicator.Infrastructure.Entities.UserSubscription", b =>
                {
                    b.HasOne("Publicator.Infrastructure.Entities.User", "SubscriberUser")
                        .WithMany("Subscriptions")
                        .HasForeignKey("SubscriberUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Publicator.Infrastructure.Entities.User", "SubscriptionUser")
                        .WithMany("Subscribers")
                        .HasForeignKey("SubscriptionUserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("Publicator.Infrastructure.Entities.UserTag", b =>
                {
                    b.HasOne("Publicator.Infrastructure.Entities.Tag", "Tag")
                        .WithMany("UserTags")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Publicator.Infrastructure.Entities.User", "User")
                        .WithMany("SubscribeTags")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Publicator.Infrastructure.Entities.Vote", b =>
                {
                    b.HasOne("Publicator.Infrastructure.Entities.Post", "Post")
                        .WithMany("Votes")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Publicator.Infrastructure.Entities.User", "User")
                        .WithMany("Votes")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}