using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Publicator.Infrastructure.Migrations
{
    public partial class AddInitialSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 32, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "States",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 32, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_States", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Nickname = table.Column<string>(maxLength: 64, nullable: true),
                    JoinDate = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    BeginStateDate = table.Column<DateTime>(nullable: false),
                    EndStateDate = table.Column<DateTime>(nullable: false),
                    PictureName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 128, nullable: true),
                    PasswordHash = table.Column<string>(maxLength: 64, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false, defaultValue: false),
                    StateId = table.Column<Guid>(nullable: false),
                    RoleId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Users_States_StateId",
                        column: x => x.StateId,
                        principalTable: "States",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Communities",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 64, nullable: true),
                    Description = table.Column<string>(maxLength: 256, nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    PictureName = table.Column<string>(maxLength: 512, nullable: true),
                    CreatorUserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Communities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Communities_Users_CreatorUserId",
                        column: x => x.CreatorUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserSubscriptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    SubscriberUserId = table.Column<Guid>(nullable: false),
                    SubscriptionUserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSubscriptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserSubscriptions_Users_SubscriberUserId",
                        column: x => x.SubscriberUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserSubscriptions_Users_SubscriptionUserId",
                        column: x => x.SubscriptionUserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserTags",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    TagId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserTags_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserTags_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 128, nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    CurrentRating = table.Column<int>(nullable: false, defaultValue: 0),
                    CreatorUserId = table.Column<Guid>(nullable: false),
                    CommunityId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Posts_Communities_CommunityId",
                        column: x => x.CommunityId,
                        principalTable: "Communities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Posts_Users_CreatorUserId",
                        column: x => x.CreatorUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserCommunities",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    CommunityId = table.Column<Guid>(nullable: false),
                    CreationDate = table.Column<DateTime>(type: "smalldatetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCommunities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserCommunities_Communities_CommunityId",
                        column: x => x.CommunityId,
                        principalTable: "Communities",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserCommunities_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Bookmarks",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreationDate = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    UserId = table.Column<Guid>(nullable: true),
                    PostId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookmarks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bookmarks_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bookmarks_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Content = table.Column<string>(nullable: true),
                    CreationDate = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    PostId = table.Column<Guid>(nullable: false),
                    ParentRepliedCommentId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Comments_ParentRepliedCommentId",
                        column: x => x.ParentRepliedCommentId,
                        principalTable: "Comments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comments_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PostTags",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    PostId = table.Column<Guid>(nullable: false),
                    TagId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostTags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostTags_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostTags_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubscriptionNewPosts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    PostId = table.Column<Guid>(nullable: false),
                    SubscriptionUserId = table.Column<Guid>(nullable: true),
                    SubscriptionTagId = table.Column<Guid>(nullable: true),
                    SubscriptionCommunityId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionNewPosts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubscriptionNewPosts_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubscriptionNewPosts_Communities_SubscriptionCommunityId",
                        column: x => x.SubscriptionCommunityId,
                        principalTable: "Communities",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SubscriptionNewPosts_Tags_SubscriptionTagId",
                        column: x => x.SubscriptionTagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SubscriptionNewPosts_Users_SubscriptionUserId",
                        column: x => x.SubscriptionUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SubscriptionNewPosts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Votes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreationDate = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    PostId = table.Column<Guid>(nullable: false),
                    Up = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Votes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Votes_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Votes_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("f795bc3b-e497-411c-836d-60c517012cf2"), "Administrator" },
                    { new Guid("de7c41e3-7e54-4e9d-8b6a-0e63f7add81d"), "Moderator" },
                    { new Guid("924ce58d-7660-4474-9fea-9ef0f49b7e16"), "Simple" }
                });

            migrationBuilder.InsertData(
                table: "States",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("27a2854d-dc5a-4892-ae5c-6bc30437bfb7"), "Active" },
                    { new Guid("87577908-c729-48f3-b09f-c04336eb3f01"), "Freezed" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "BeginStateDate", "Email", "EndStateDate", "JoinDate", "Nickname", "PasswordHash", "PictureName", "RoleId", "StateId" },
                values: new object[] { new Guid("9d2d2d20-477a-4505-bd84-a3938d69d1f2"), new DateTime(2020, 5, 23, 3, 55, 35, 898, DateTimeKind.Local).AddTicks(6515), "lineyk27gg@gmail.com", new DateTime(2050, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 5, 23, 3, 55, 35, 893, DateTimeKind.Local).AddTicks(4011), "lineyk27", "ef797c8118f02dfb649607dd5d3f8c7623048c9c063d532cc95c5ed7a898a64f", "https://res.cloudinary.com/dgepkksyl/image/upload/v1590192208/user_pics/IMG_0572_ut5cxl.jpg", new Guid("924ce58d-7660-4474-9fea-9ef0f49b7e16"), new Guid("27a2854d-dc5a-4892-ae5c-6bc30437bfb7") });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "BeginStateDate", "Email", "EndStateDate", "JoinDate", "Nickname", "PasswordHash", "PictureName", "RoleId", "StateId" },
                values: new object[] { new Guid("46df7653-85fb-458b-b092-6efcc2ef8466"), new DateTime(2020, 5, 23, 3, 55, 35, 899, DateTimeKind.Local).AddTicks(1428), "lineyk27@gmail.com", new DateTime(2050, 12, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 5, 23, 3, 55, 35, 899, DateTimeKind.Local).AddTicks(1389), "kit22", "5e884898da28047151d0e56f8dc6292773603d0d6aabbdd62a11ef721d1542d8", "https://res.cloudinary.com/dgepkksyl/image/upload/v1590194554/user_pics/index_gz1sqk.jpg", new Guid("924ce58d-7660-4474-9fea-9ef0f49b7e16"), new Guid("27a2854d-dc5a-4892-ae5c-6bc30437bfb7") });

            migrationBuilder.InsertData(
                table: "Communities",
                columns: new[] { "Id", "CreationDate", "CreatorUserId", "Description", "Name", "PictureName" },
                values: new object[,]
                {
                    { new Guid("b3f45e42-ef6f-45ae-9b21-e2fba6bb7d3e"), new DateTime(2020, 5, 23, 3, 55, 35, 899, DateTimeKind.Local).AddTicks(3439), new Guid("9d2d2d20-477a-4505-bd84-a3938d69d1f2"), "Community for car lovers.", "Cars", "https://res.cloudinary.com/dgepkksyl/image/upload/v1590193947/user_pics/cars2_c5wyqk.jpg" },
                    { new Guid("48c6645d-3fa4-42a0-9753-ca6917fd56c3"), new DateTime(2020, 5, 23, 3, 55, 35, 899, DateTimeKind.Local).AddTicks(5498), new Guid("9d2d2d20-477a-4505-bd84-a3938d69d1f2"), "Community footbal, basketall, tenis and other.", "Sport", "https://res.cloudinary.com/dgepkksyl/image/upload/v1590193942/user_pics/sport2_fs0yab.jpg" },
                    { new Guid("52ebf60b-f971-4895-b374-9eaa8cdc0870"), new DateTime(2020, 5, 23, 3, 55, 35, 899, DateTimeKind.Local).AddTicks(5566), new Guid("9d2d2d20-477a-4505-bd84-a3938d69d1f2"), "Community for those who love cooking.", "Food", "https://res.cloudinary.com/dgepkksyl/image/upload/v1590193991/user_pics/food2_uckrcb.jpg" },
                    { new Guid("c31b4695-c41b-4596-9446-1e875fe90d4a"), new DateTime(2020, 5, 23, 3, 55, 35, 899, DateTimeKind.Local).AddTicks(5575), new Guid("9d2d2d20-477a-4505-bd84-a3938d69d1f2"), "IT industry communicates here.", "Programming", "https://res.cloudinary.com/dgepkksyl/image/upload/v1590193952/user_pics/programing_wlmjj4.jpg" },
                    { new Guid("88420b23-b6e7-42dd-b975-a3924bbaa271"), new DateTime(2020, 5, 23, 3, 55, 35, 899, DateTimeKind.Local).AddTicks(5580), new Guid("9d2d2d20-477a-4505-bd84-a3938d69d1f2"), "Culture life.", "Culture", "https://res.cloudinary.com/dgepkksyl/image/upload/v1590193976/user_pics/culture2_hdclpb.jpg" }
                });

            migrationBuilder.InsertData(
                table: "UserSubscriptions",
                columns: new[] { "Id", "SubscriberUserId", "SubscriptionUserId" },
                values: new object[] { new Guid("0be926b0-1071-4058-a697-6da3d6d4af33"), new Guid("9d2d2d20-477a-4505-bd84-a3938d69d1f2"), new Guid("46df7653-85fb-458b-b092-6efcc2ef8466") });

            migrationBuilder.CreateIndex(
                name: "IX_Bookmarks_PostId",
                table: "Bookmarks",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookmarks_UserId",
                table: "Bookmarks",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ParentRepliedCommentId",
                table: "Comments",
                column: "ParentRepliedCommentId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_PostId",
                table: "Comments",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UserId",
                table: "Comments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Communities_CreatorUserId",
                table: "Communities",
                column: "CreatorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_CommunityId",
                table: "Posts",
                column: "CommunityId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_CreatorUserId",
                table: "Posts",
                column: "CreatorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_PostTags_PostId",
                table: "PostTags",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_PostTags_TagId",
                table: "PostTags",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionNewPosts_PostId",
                table: "SubscriptionNewPosts",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionNewPosts_SubscriptionCommunityId",
                table: "SubscriptionNewPosts",
                column: "SubscriptionCommunityId");

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionNewPosts_SubscriptionTagId",
                table: "SubscriptionNewPosts",
                column: "SubscriptionTagId");

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionNewPosts_SubscriptionUserId",
                table: "SubscriptionNewPosts",
                column: "SubscriptionUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionNewPosts_UserId",
                table: "SubscriptionNewPosts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCommunities_CommunityId",
                table: "UserCommunities",
                column: "CommunityId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCommunities_UserId",
                table: "UserCommunities",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_StateId",
                table: "Users",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSubscriptions_SubscriberUserId",
                table: "UserSubscriptions",
                column: "SubscriberUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSubscriptions_SubscriptionUserId",
                table: "UserSubscriptions",
                column: "SubscriptionUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTags_TagId",
                table: "UserTags",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTags_UserId",
                table: "UserTags",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Votes_PostId",
                table: "Votes",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_Votes_UserId",
                table: "Votes",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bookmarks");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "PostTags");

            migrationBuilder.DropTable(
                name: "SubscriptionNewPosts");

            migrationBuilder.DropTable(
                name: "UserCommunities");

            migrationBuilder.DropTable(
                name: "UserSubscriptions");

            migrationBuilder.DropTable(
                name: "UserTags");

            migrationBuilder.DropTable(
                name: "Votes");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "Communities");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "States");
        }
    }
}
