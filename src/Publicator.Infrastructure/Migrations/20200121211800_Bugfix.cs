using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Publicator.Infrastructure.Migrations
{
    public partial class Bugfix : Migration
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
                    PictureName = table.Column<string>(maxLength: 64, nullable: true),
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
                    Content = table.Column<string>(type: "ntext", nullable: true),
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
                    SubscriptionCommunityId = table.Column<Guid>(nullable: false)
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
                    Up = table.Column<bool>(nullable: false, defaultValue: true)
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
                    { new Guid("ceec90a5-821f-485d-a812-847be50260b4"), "Administrator" },
                    { new Guid("282db597-0df6-4f49-9832-3dedd27ee818"), "Moderator" },
                    { new Guid("88d99ed8-8e71-4123-b235-d179db86ccdd"), "Simple" }
                });

            migrationBuilder.InsertData(
                table: "States",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("d9eafe03-b6cb-4bf8-bc19-197621eed68e"), "Active" },
                    { new Guid("32cf6a4d-241e-4138-9927-7cee554024fe"), "Freezed" }
                });

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("eab2c81c-cd7e-4f7d-ba79-686a3dd8a61e"), "Life" },
                    { new Guid("f5625976-84c0-4ece-ae08-fdf975417951"), "Motorcycle" },
                    { new Guid("5d6de9ac-6c8e-4c15-b0bb-bd9020c7f325"), "Car" },
                    { new Guid("d6ab3a80-9321-46a9-829d-388e0a6c47d5"), "Politic" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "BeginStateDate", "Email", "EndStateDate", "JoinDate", "Nickname", "PasswordHash", "PictureName", "RoleId", "StateId" },
                values: new object[] { new Guid("3de9d387-4cf9-44e4-a0af-0395f4531c86"), new DateTime(2020, 1, 21, 23, 17, 59, 244, DateTimeKind.Local).AddTicks(6066), "lineyk27gg@gmail.com", new DateTime(2050, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 1, 21, 23, 17, 59, 239, DateTimeKind.Local).AddTicks(4199), "lineyk27gg", "ef797c8118f02dfb649607dd5d3f8c7623048c9c063d532cc95c5ed7a898a64f", null, new Guid("88d99ed8-8e71-4123-b235-d179db86ccdd"), new Guid("d9eafe03-b6cb-4bf8-bc19-197621eed68e") });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "BeginStateDate", "Email", "EndStateDate", "JoinDate", "Nickname", "PasswordHash", "PictureName", "RoleId", "StateId" },
                values: new object[] { new Guid("011438e9-5080-451f-9149-559d658b7b25"), new DateTime(2020, 1, 21, 23, 17, 59, 245, DateTimeKind.Local).AddTicks(510), "lineyk27@yandex.ru", new DateTime(2050, 12, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 1, 21, 23, 17, 59, 245, DateTimeKind.Local).AddTicks(475), "lineyk27", "5e884898da28047151d0e56f8dc6292773603d0d6aabbdd62a11ef721d1542d8", null, new Guid("88d99ed8-8e71-4123-b235-d179db86ccdd"), new Guid("d9eafe03-b6cb-4bf8-bc19-197621eed68e") });

            migrationBuilder.InsertData(
                table: "Communities",
                columns: new[] { "Id", "CreationDate", "CreatorUserId", "Description", "Name", "PictureName" },
                values: new object[] { new Guid("34e68237-4a11-41ef-97fc-717ae3d13e6c"), new DateTime(2020, 1, 21, 23, 17, 59, 245, DateTimeKind.Local).AddTicks(2581), new Guid("3de9d387-4cf9-44e4-a0af-0395f4531c86"), "Here users post stories about theirs lifes.", "Stories about Life", null });

            migrationBuilder.InsertData(
                table: "Communities",
                columns: new[] { "Id", "CreationDate", "CreatorUserId", "Description", "Name", "PictureName" },
                values: new object[] { new Guid("2b828160-0315-4341-aaa3-a971b72c2139"), new DateTime(2020, 1, 21, 23, 17, 59, 245, DateTimeKind.Local).AddTicks(4034), new Guid("3de9d387-4cf9-44e4-a0af-0395f4531c86"), "Community about vehicles and all about it.", "Vehicle", null });

            migrationBuilder.InsertData(
                table: "UserSubscriptions",
                columns: new[] { "Id", "SubscriberUserId", "SubscriptionUserId" },
                values: new object[] { new Guid("8edeb646-d687-4b84-acf7-836c54a0befd"), new Guid("3de9d387-4cf9-44e4-a0af-0395f4531c86"), new Guid("011438e9-5080-451f-9149-559d658b7b25") });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "CommunityId", "Content", "CreationDate", "CreatorUserId", "Name" },
                values: new object[] { new Guid("80b5abde-0ddd-4331-8fcd-44a93e3ddcd6"), new Guid("34e68237-4a11-41ef-97fc-717ae3d13e6c"), "<p>In this post i want to tell you story of my life...</p>", new DateTime(2020, 1, 21, 23, 17, 59, 245, DateTimeKind.Local).AddTicks(6710), new Guid("3de9d387-4cf9-44e4-a0af-0395f4531c86"), "Post about my life" });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "CommunityId", "Content", "CreationDate", "CreatorUserId", "Name" },
                values: new object[] { new Guid("9b9fe4fc-b300-4651-85ca-d92c4d599e18"), new Guid("34e68237-4a11-41ef-97fc-717ae3d13e6c"), "<p>Once upon a time in Ukraine, village Sokal i studied in school...</p>", new DateTime(2020, 1, 21, 23, 17, 59, 246, DateTimeKind.Local).AddTicks(280), new Guid("011438e9-5080-451f-9149-559d658b7b25"), "Happened in Sokal" });

            migrationBuilder.InsertData(
                table: "Bookmarks",
                columns: new[] { "Id", "CreationDate", "PostId", "UserId" },
                values: new object[] { new Guid("ef49b3bb-1c77-4347-9a12-744e0505d559"), new DateTime(2020, 1, 24, 23, 17, 59, 246, DateTimeKind.Local).AddTicks(5327), new Guid("9b9fe4fc-b300-4651-85ca-d92c4d599e18"), new Guid("3de9d387-4cf9-44e4-a0af-0395f4531c86") });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "Content", "CreationDate", "ParentRepliedCommentId", "PostId", "UserId" },
                values: new object[] { new Guid("67d1b6d8-e005-4573-b6c2-c64b29df652f"), "<p>Hilarious, how it happened with you</p>", new DateTime(2020, 1, 22, 23, 17, 59, 246, DateTimeKind.Local).AddTicks(2469), null, new Guid("80b5abde-0ddd-4331-8fcd-44a93e3ddcd6"), new Guid("3de9d387-4cf9-44e4-a0af-0395f4531c86") });

            migrationBuilder.InsertData(
                table: "PostTags",
                columns: new[] { "Id", "PostId", "TagId" },
                values: new object[,]
                {
                    { new Guid("869170b6-3a1a-471a-8059-9a3a49672821"), new Guid("80b5abde-0ddd-4331-8fcd-44a93e3ddcd6"), new Guid("eab2c81c-cd7e-4f7d-ba79-686a3dd8a61e") },
                    { new Guid("fad7973a-7b3a-4fd2-ba91-029e16818ff8"), new Guid("80b5abde-0ddd-4331-8fcd-44a93e3ddcd6"), new Guid("d6ab3a80-9321-46a9-829d-388e0a6c47d5") }
                });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "Content", "CreationDate", "ParentRepliedCommentId", "PostId", "UserId" },
                values: new object[] { new Guid("9c5c6d31-ca66-48a0-abd4-1086f900ed78"), "<p>It happened in 2016 year, i was 16 years old</p>", new DateTime(2020, 1, 23, 23, 17, 59, 246, DateTimeKind.Local).AddTicks(3915), new Guid("67d1b6d8-e005-4573-b6c2-c64b29df652f"), new Guid("80b5abde-0ddd-4331-8fcd-44a93e3ddcd6"), new Guid("011438e9-5080-451f-9149-559d658b7b25") });

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
