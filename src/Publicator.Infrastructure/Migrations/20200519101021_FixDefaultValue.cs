using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Publicator.Infrastructure.Migrations
{
    public partial class FixDefaultValue : Migration
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
                    { new Guid("14172959-9fca-4765-853d-f166b61df95b"), "Administrator" },
                    { new Guid("f9f5daf5-7677-4740-bf9c-b82a5af6e16f"), "Moderator" },
                    { new Guid("29adbc34-52b8-4769-b437-2797fed3dfa3"), "Simple" }
                });

            migrationBuilder.InsertData(
                table: "States",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("77a3ac34-9f4a-4c98-9a69-8c2382e69d14"), "Active" },
                    { new Guid("bc0eb7b6-0c30-40b0-86f1-ecbca8360d1e"), "Freezed" }
                });

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("2b8d5e1c-fdc0-4471-974d-c155a9f18f55"), "Life" },
                    { new Guid("756eb495-6529-477c-b73b-68c24b13e1cd"), "Motorcycle" },
                    { new Guid("c2ebc6b1-4ef2-482a-9b25-4e2670ff3a3d"), "Car" },
                    { new Guid("72a9e71e-7a76-4922-b486-656b419cf9ca"), "Politic" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "BeginStateDate", "Email", "EndStateDate", "JoinDate", "Nickname", "PasswordHash", "PictureName", "RoleId", "StateId" },
                values: new object[] { new Guid("c255818e-574f-4b07-86f5-8d5dff4021f1"), new DateTime(2020, 5, 19, 13, 10, 20, 660, DateTimeKind.Local).AddTicks(396), "lineyk27gg@gmail.com", new DateTime(2050, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 5, 19, 13, 10, 20, 653, DateTimeKind.Local).AddTicks(59), "lineyk27gg", "ef797c8118f02dfb649607dd5d3f8c7623048c9c063d532cc95c5ed7a898a64f", null, new Guid("29adbc34-52b8-4769-b437-2797fed3dfa3"), new Guid("77a3ac34-9f4a-4c98-9a69-8c2382e69d14") });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "BeginStateDate", "Email", "EndStateDate", "JoinDate", "Nickname", "PasswordHash", "PictureName", "RoleId", "StateId" },
                values: new object[] { new Guid("7235142b-bf61-46d6-a844-af2613e94c3d"), new DateTime(2020, 5, 19, 13, 10, 20, 660, DateTimeKind.Local).AddTicks(5416), "lineyk27@yandex.ru", new DateTime(2050, 12, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 5, 19, 13, 10, 20, 660, DateTimeKind.Local).AddTicks(5380), "lineyk27", "5e884898da28047151d0e56f8dc6292773603d0d6aabbdd62a11ef721d1542d8", null, new Guid("29adbc34-52b8-4769-b437-2797fed3dfa3"), new Guid("77a3ac34-9f4a-4c98-9a69-8c2382e69d14") });

            migrationBuilder.InsertData(
                table: "Communities",
                columns: new[] { "Id", "CreationDate", "CreatorUserId", "Description", "Name", "PictureName" },
                values: new object[] { new Guid("793ae715-ebc1-4aee-bcbf-16dcbca0d4f3"), new DateTime(2020, 5, 19, 13, 10, 20, 660, DateTimeKind.Local).AddTicks(8032), new Guid("c255818e-574f-4b07-86f5-8d5dff4021f1"), "Here users post stories about theirs lifes.", "Stories about Life", null });

            migrationBuilder.InsertData(
                table: "Communities",
                columns: new[] { "Id", "CreationDate", "CreatorUserId", "Description", "Name", "PictureName" },
                values: new object[] { new Guid("236ecced-2c1b-4aa6-b386-d831accabc01"), new DateTime(2020, 5, 19, 13, 10, 20, 660, DateTimeKind.Local).AddTicks(9954), new Guid("c255818e-574f-4b07-86f5-8d5dff4021f1"), "Community about vehicles and all about it.", "Vehicle", null });

            migrationBuilder.InsertData(
                table: "UserSubscriptions",
                columns: new[] { "Id", "SubscriberUserId", "SubscriptionUserId" },
                values: new object[] { new Guid("c5ff1a0d-9c02-44ec-8756-8e5385498798"), new Guid("c255818e-574f-4b07-86f5-8d5dff4021f1"), new Guid("7235142b-bf61-46d6-a844-af2613e94c3d") });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "CommunityId", "Content", "CreationDate", "CreatorUserId", "Name" },
                values: new object[] { new Guid("1c146bb4-18a1-4c26-83d7-eca5221ae591"), new Guid("793ae715-ebc1-4aee-bcbf-16dcbca0d4f3"), "<p>In this post i want to tell you story of my life...</p>", new DateTime(2020, 5, 19, 13, 10, 20, 661, DateTimeKind.Local).AddTicks(2451), new Guid("c255818e-574f-4b07-86f5-8d5dff4021f1"), "Post about my life" });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "CommunityId", "Content", "CreationDate", "CreatorUserId", "Name" },
                values: new object[] { new Guid("e24c7135-0d47-4875-924d-8517991b7d16"), new Guid("793ae715-ebc1-4aee-bcbf-16dcbca0d4f3"), "<p>Once upon a time in Ukraine, village Sokal i studied in school...</p>", new DateTime(2020, 5, 19, 13, 10, 20, 661, DateTimeKind.Local).AddTicks(5297), new Guid("7235142b-bf61-46d6-a844-af2613e94c3d"), "Happened in Sokal" });

            migrationBuilder.InsertData(
                table: "Bookmarks",
                columns: new[] { "Id", "CreationDate", "PostId", "UserId" },
                values: new object[] { new Guid("5e06b34b-563c-45cf-a257-942722bcec01"), new DateTime(2020, 5, 22, 13, 10, 20, 662, DateTimeKind.Local).AddTicks(1576), new Guid("e24c7135-0d47-4875-924d-8517991b7d16"), new Guid("c255818e-574f-4b07-86f5-8d5dff4021f1") });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "Content", "CreationDate", "ParentRepliedCommentId", "PostId", "UserId" },
                values: new object[] { new Guid("b98aab3d-ec1f-4745-a530-ecedfd5b67ee"), "<p>Hilarious, how it happened with you</p>", new DateTime(2020, 5, 20, 13, 10, 20, 661, DateTimeKind.Local).AddTicks(7851), null, new Guid("1c146bb4-18a1-4c26-83d7-eca5221ae591"), new Guid("c255818e-574f-4b07-86f5-8d5dff4021f1") });

            migrationBuilder.InsertData(
                table: "PostTags",
                columns: new[] { "Id", "PostId", "TagId" },
                values: new object[,]
                {
                    { new Guid("36baf4a3-7a19-491e-bb6b-0d411a4f5bb8"), new Guid("1c146bb4-18a1-4c26-83d7-eca5221ae591"), new Guid("2b8d5e1c-fdc0-4471-974d-c155a9f18f55") },
                    { new Guid("559905cf-b051-4153-a24a-d52d3dd10cf7"), new Guid("1c146bb4-18a1-4c26-83d7-eca5221ae591"), new Guid("72a9e71e-7a76-4922-b486-656b419cf9ca") }
                });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "Content", "CreationDate", "ParentRepliedCommentId", "PostId", "UserId" },
                values: new object[] { new Guid("3b44310f-904f-4aff-b19b-35952fd93a20"), "<p>It happened in 2016 year, i was 16 years old</p>", new DateTime(2020, 5, 21, 13, 10, 20, 661, DateTimeKind.Local).AddTicks(9653), new Guid("b98aab3d-ec1f-4745-a530-ecedfd5b67ee"), new Guid("1c146bb4-18a1-4c26-83d7-eca5221ae591"), new Guid("7235142b-bf61-46d6-a844-af2613e94c3d") });

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
