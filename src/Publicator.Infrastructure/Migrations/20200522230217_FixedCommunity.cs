using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Publicator.Infrastructure.Migrations
{
    public partial class FixedCommunity : Migration
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
                    Description = table.Column<string>(maxLength: 128, nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    PictureName = table.Column<string>(maxLength: 32, nullable: true),
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
                    { new Guid("e5d75f6f-403f-4758-b7ba-274f7954a71e"), "Administrator" },
                    { new Guid("cc40c606-dc85-4e11-a599-f5e69ec3cade"), "Moderator" },
                    { new Guid("4fc3330e-151c-497e-ad7a-01b4fcc7ef88"), "Simple" }
                });

            migrationBuilder.InsertData(
                table: "States",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("87bd1bee-0def-40f7-93a0-8666ac0a0628"), "Active" },
                    { new Guid("3839a9c0-a469-4fb3-8154-2ba1c715465a"), "Freezed" }
                });

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("063b969c-f8c2-4c5d-a0af-0491a125e7b0"), "Life" },
                    { new Guid("18b5ecf8-3c94-41b2-b7fb-15d7c6a79e8c"), "Motorcycle" },
                    { new Guid("21577f69-1aca-4359-98ac-8df961579496"), "Car" },
                    { new Guid("0bc36acb-cdb8-4824-b113-832a8a8682f9"), "Politic" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "BeginStateDate", "Email", "EndStateDate", "JoinDate", "Nickname", "PasswordHash", "PictureName", "RoleId", "StateId" },
                values: new object[] { new Guid("465fc7ac-91fa-4089-8567-6253810ba15c"), new DateTime(2020, 5, 23, 2, 2, 16, 50, DateTimeKind.Local).AddTicks(8523), "lineyk27gg@gmail.com", new DateTime(2050, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 5, 23, 2, 2, 16, 42, DateTimeKind.Local).AddTicks(7153), "lineyk27gg", "ef797c8118f02dfb649607dd5d3f8c7623048c9c063d532cc95c5ed7a898a64f", null, new Guid("4fc3330e-151c-497e-ad7a-01b4fcc7ef88"), new Guid("87bd1bee-0def-40f7-93a0-8666ac0a0628") });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "BeginStateDate", "Email", "EndStateDate", "JoinDate", "Nickname", "PasswordHash", "PictureName", "RoleId", "StateId" },
                values: new object[] { new Guid("e473d454-6146-4e62-94fd-58652140f17a"), new DateTime(2020, 5, 23, 2, 2, 16, 51, DateTimeKind.Local).AddTicks(6428), "lineyk27@yandex.ru", new DateTime(2050, 12, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 5, 23, 2, 2, 16, 51, DateTimeKind.Local).AddTicks(6334), "lineyk27", "5e884898da28047151d0e56f8dc6292773603d0d6aabbdd62a11ef721d1542d8", null, new Guid("4fc3330e-151c-497e-ad7a-01b4fcc7ef88"), new Guid("87bd1bee-0def-40f7-93a0-8666ac0a0628") });

            migrationBuilder.InsertData(
                table: "Communities",
                columns: new[] { "Id", "CreationDate", "CreatorUserId", "Description", "Name", "PictureName" },
                values: new object[] { new Guid("3f9f220f-996c-492b-b009-5f9426ab58e5"), new DateTime(2020, 5, 23, 2, 2, 16, 52, DateTimeKind.Local).AddTicks(705), new Guid("465fc7ac-91fa-4089-8567-6253810ba15c"), "Here users post stories about theirs lifes.", "Stories about Life", null });

            migrationBuilder.InsertData(
                table: "Communities",
                columns: new[] { "Id", "CreationDate", "CreatorUserId", "Description", "Name", "PictureName" },
                values: new object[] { new Guid("f47a873e-038f-41fb-b030-b94fe4c52d1e"), new DateTime(2020, 5, 23, 2, 2, 16, 52, DateTimeKind.Local).AddTicks(4001), new Guid("465fc7ac-91fa-4089-8567-6253810ba15c"), "Community about vehicles and all about it.", "Vehicle", null });

            migrationBuilder.InsertData(
                table: "UserSubscriptions",
                columns: new[] { "Id", "SubscriberUserId", "SubscriptionUserId" },
                values: new object[] { new Guid("0064891c-a520-431a-83f2-549b3109e054"), new Guid("465fc7ac-91fa-4089-8567-6253810ba15c"), new Guid("e473d454-6146-4e62-94fd-58652140f17a") });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "CommunityId", "Content", "CreationDate", "CreatorUserId", "Name" },
                values: new object[] { new Guid("f11250c9-b495-403f-a199-ce2aab43e5ee"), new Guid("3f9f220f-996c-492b-b009-5f9426ab58e5"), "<p>In this post i want to tell you story of my life...</p>", new DateTime(2020, 5, 23, 2, 2, 16, 52, DateTimeKind.Local).AddTicks(8402), new Guid("465fc7ac-91fa-4089-8567-6253810ba15c"), "Post about my life" });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "CommunityId", "Content", "CreationDate", "CreatorUserId", "Name" },
                values: new object[] { new Guid("148dbbdc-47a6-493b-a07b-28b76fd7c924"), new Guid("3f9f220f-996c-492b-b009-5f9426ab58e5"), "<p>Once upon a time in Ukraine, village Sokal i studied in school...</p>", new DateTime(2020, 5, 23, 2, 2, 16, 53, DateTimeKind.Local).AddTicks(3301), new Guid("e473d454-6146-4e62-94fd-58652140f17a"), "Happened in Sokal" });

            migrationBuilder.InsertData(
                table: "Bookmarks",
                columns: new[] { "Id", "CreationDate", "PostId", "UserId" },
                values: new object[] { new Guid("f7d3335d-d586-46f7-8c3a-82e1315c1b46"), new DateTime(2020, 5, 26, 2, 2, 16, 54, DateTimeKind.Local).AddTicks(3631), new Guid("148dbbdc-47a6-493b-a07b-28b76fd7c924"), new Guid("465fc7ac-91fa-4089-8567-6253810ba15c") });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "Content", "CreationDate", "ParentRepliedCommentId", "PostId", "UserId" },
                values: new object[] { new Guid("a847aefb-6833-49e6-88cd-07c3eb46551d"), "<p>Hilarious, how it happened with you</p>", new DateTime(2020, 5, 24, 2, 2, 16, 53, DateTimeKind.Local).AddTicks(7532), null, new Guid("f11250c9-b495-403f-a199-ce2aab43e5ee"), new Guid("465fc7ac-91fa-4089-8567-6253810ba15c") });

            migrationBuilder.InsertData(
                table: "PostTags",
                columns: new[] { "Id", "PostId", "TagId" },
                values: new object[,]
                {
                    { new Guid("1f3f774a-f531-4e3c-b8d7-7749616a4ff8"), new Guid("f11250c9-b495-403f-a199-ce2aab43e5ee"), new Guid("063b969c-f8c2-4c5d-a0af-0491a125e7b0") },
                    { new Guid("0cb4622a-d22c-40ea-bbb2-d461ca748efc"), new Guid("f11250c9-b495-403f-a199-ce2aab43e5ee"), new Guid("0bc36acb-cdb8-4824-b113-832a8a8682f9") }
                });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "Content", "CreationDate", "ParentRepliedCommentId", "PostId", "UserId" },
                values: new object[] { new Guid("2e64e124-472f-43bc-8e07-3043c677d47a"), "<p>It happened in 2016 year, i was 16 years old</p>", new DateTime(2020, 5, 25, 2, 2, 16, 54, DateTimeKind.Local).AddTicks(616), new Guid("a847aefb-6833-49e6-88cd-07c3eb46551d"), new Guid("f11250c9-b495-403f-a199-ce2aab43e5ee"), new Guid("e473d454-6146-4e62-94fd-58652140f17a") });

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
