using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Publicator.Infrastructure.Migrations
{
    public partial class BugfixTextLength : Migration
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
                    { new Guid("e6afcebe-eb6b-425a-bee2-835497b9ac99"), "Administrator" },
                    { new Guid("76170001-5c3d-453c-a2c8-89617d61d0d2"), "Moderator" },
                    { new Guid("2677745d-48c4-424b-b5d2-98bc47b47aa3"), "Simple" }
                });

            migrationBuilder.InsertData(
                table: "States",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("30f367c3-7b13-4d5a-b9a1-fb57aca165c5"), "Active" },
                    { new Guid("9b44ae28-17ab-4663-b397-3860a83a5284"), "Freezed" }
                });

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("a29b5428-55b0-4518-a3de-1dfa6dbbba5b"), "Life" },
                    { new Guid("e87ce936-94ba-42d0-a780-ca622920e2ac"), "Motorcycle" },
                    { new Guid("eb044179-5072-49cf-ac94-4c2b66e3f791"), "Car" },
                    { new Guid("6ba013f1-1690-4804-b666-a426419b6a34"), "Politic" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "BeginStateDate", "Email", "EndStateDate", "JoinDate", "Nickname", "PasswordHash", "PictureName", "RoleId", "StateId" },
                values: new object[] { new Guid("afbc4e51-5279-4d93-ab05-40cff74088b2"), new DateTime(2020, 1, 25, 21, 43, 23, 960, DateTimeKind.Local).AddTicks(4198), "lineyk27gg@gmail.com", new DateTime(2050, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 1, 25, 21, 43, 23, 954, DateTimeKind.Local).AddTicks(8070), "lineyk27gg", "ef797c8118f02dfb649607dd5d3f8c7623048c9c063d532cc95c5ed7a898a64f", null, new Guid("2677745d-48c4-424b-b5d2-98bc47b47aa3"), new Guid("30f367c3-7b13-4d5a-b9a1-fb57aca165c5") });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "BeginStateDate", "Email", "EndStateDate", "JoinDate", "Nickname", "PasswordHash", "PictureName", "RoleId", "StateId" },
                values: new object[] { new Guid("1372c680-c3ec-4d3a-b45c-306c1825d5b0"), new DateTime(2020, 1, 25, 21, 43, 23, 960, DateTimeKind.Local).AddTicks(8702), "lineyk27@yandex.ru", new DateTime(2050, 12, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 1, 25, 21, 43, 23, 960, DateTimeKind.Local).AddTicks(8661), "lineyk27", "5e884898da28047151d0e56f8dc6292773603d0d6aabbdd62a11ef721d1542d8", null, new Guid("2677745d-48c4-424b-b5d2-98bc47b47aa3"), new Guid("30f367c3-7b13-4d5a-b9a1-fb57aca165c5") });

            migrationBuilder.InsertData(
                table: "Communities",
                columns: new[] { "Id", "CreationDate", "CreatorUserId", "Description", "Name", "PictureName" },
                values: new object[] { new Guid("ad88a4e6-2a41-427c-9a92-d08712547ba6"), new DateTime(2020, 1, 25, 21, 43, 23, 961, DateTimeKind.Local).AddTicks(1002), new Guid("afbc4e51-5279-4d93-ab05-40cff74088b2"), "Here users post stories about theirs lifes.", "Stories about Life", null });

            migrationBuilder.InsertData(
                table: "Communities",
                columns: new[] { "Id", "CreationDate", "CreatorUserId", "Description", "Name", "PictureName" },
                values: new object[] { new Guid("30c42092-2bba-4dbb-917c-8bbb6c39c2ce"), new DateTime(2020, 1, 25, 21, 43, 23, 961, DateTimeKind.Local).AddTicks(2608), new Guid("afbc4e51-5279-4d93-ab05-40cff74088b2"), "Community about vehicles and all about it.", "Vehicle", null });

            migrationBuilder.InsertData(
                table: "UserSubscriptions",
                columns: new[] { "Id", "SubscriberUserId", "SubscriptionUserId" },
                values: new object[] { new Guid("3470e86b-9a6d-4612-92b3-4cbc4dbe297f"), new Guid("afbc4e51-5279-4d93-ab05-40cff74088b2"), new Guid("1372c680-c3ec-4d3a-b45c-306c1825d5b0") });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "CommunityId", "Content", "CreationDate", "CreatorUserId", "Name" },
                values: new object[] { new Guid("63001e62-4a0d-42af-b701-bcbf1ed657e6"), new Guid("ad88a4e6-2a41-427c-9a92-d08712547ba6"), "<p>In this post i want to tell you story of my life...</p>", new DateTime(2020, 1, 25, 21, 43, 23, 961, DateTimeKind.Local).AddTicks(4764), new Guid("afbc4e51-5279-4d93-ab05-40cff74088b2"), "Post about my life" });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "CommunityId", "Content", "CreationDate", "CreatorUserId", "Name" },
                values: new object[] { new Guid("5c22e9bb-d706-442c-93c4-0dee89cfe7d1"), new Guid("ad88a4e6-2a41-427c-9a92-d08712547ba6"), "<p>Once upon a time in Ukraine, village Sokal i studied in school...</p>", new DateTime(2020, 1, 25, 21, 43, 23, 961, DateTimeKind.Local).AddTicks(7272), new Guid("1372c680-c3ec-4d3a-b45c-306c1825d5b0"), "Happened in Sokal" });

            migrationBuilder.InsertData(
                table: "Bookmarks",
                columns: new[] { "Id", "CreationDate", "PostId", "UserId" },
                values: new object[] { new Guid("78985e67-8120-475e-83ba-7d7e1d7c872a"), new DateTime(2020, 1, 28, 21, 43, 23, 962, DateTimeKind.Local).AddTicks(2875), new Guid("5c22e9bb-d706-442c-93c4-0dee89cfe7d1"), new Guid("afbc4e51-5279-4d93-ab05-40cff74088b2") });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "Content", "CreationDate", "ParentRepliedCommentId", "PostId", "UserId" },
                values: new object[] { new Guid("e546d420-a130-4f8a-ac49-64fac2f32f82"), "<p>Hilarious, how it happened with you</p>", new DateTime(2020, 1, 26, 21, 43, 23, 961, DateTimeKind.Local).AddTicks(9618), null, new Guid("63001e62-4a0d-42af-b701-bcbf1ed657e6"), new Guid("afbc4e51-5279-4d93-ab05-40cff74088b2") });

            migrationBuilder.InsertData(
                table: "PostTags",
                columns: new[] { "Id", "PostId", "TagId" },
                values: new object[,]
                {
                    { new Guid("eb3db4ca-1161-4e99-9414-085ce3e5e9b3"), new Guid("63001e62-4a0d-42af-b701-bcbf1ed657e6"), new Guid("a29b5428-55b0-4518-a3de-1dfa6dbbba5b") },
                    { new Guid("34e2ad48-31d4-43b4-9339-e679a46f974e"), new Guid("63001e62-4a0d-42af-b701-bcbf1ed657e6"), new Guid("6ba013f1-1690-4804-b666-a426419b6a34") }
                });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "Content", "CreationDate", "ParentRepliedCommentId", "PostId", "UserId" },
                values: new object[] { new Guid("2b425d64-3325-4585-9dc6-c01dc2f8841b"), "<p>It happened in 2016 year, i was 16 years old</p>", new DateTime(2020, 1, 27, 21, 43, 23, 962, DateTimeKind.Local).AddTicks(1172), new Guid("e546d420-a130-4f8a-ac49-64fac2f32f82"), new Guid("63001e62-4a0d-42af-b701-bcbf1ed657e6"), new Guid("1372c680-c3ec-4d3a-b45c-306c1825d5b0") });

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
