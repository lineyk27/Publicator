using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Publicator.Infrastructure.Migrations
{
    public partial class AddIdentityWithSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "JoinDate", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PictureUrl", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("f71f74d5-4f5a-4947-9550-9b1488d29544"), 0, "b0a67828-b4ab-447e-8a67-bf655b316d44", "lineyk27gg@gmail.com", false, new DateTime(2020, 10, 6, 13, 6, 47, 167, DateTimeKind.Local).AddTicks(2501), false, null, null, null, "ef797c8118f02dfb649607dd5d3f8c7623048c9c063d532cc95c5ed7a898a64f", null, false, "https://res.cloudinary.com/dgepkksyl/image/upload/v1590192208/user_pics/IMG_0572_ut5cxl.jpg", null, false, "lineyk27" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "JoinDate", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PictureUrl", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("61175164-ed74-4605-bf09-d22fcb4d72e7"), 0, "c004b704-2a5f-4081-ac87-2378f62e51bb", "lineyk27@gmail.com", false, new DateTime(2020, 10, 6, 13, 6, 47, 174, DateTimeKind.Local).AddTicks(6442), false, null, null, null, "5e884898da28047151d0e56f8dc6292773603d0d6aabbdd62a11ef721d1542d8", null, false, "https://res.cloudinary.com/dgepkksyl/image/upload/v1590194554/user_pics/index_gz1sqk.jpg", null, false, "kit22" });

            migrationBuilder.InsertData(
                table: "Communities",
                columns: new[] { "Id", "CreationDate", "CreatorUserId", "Description", "Name", "PictureUrl" },
                values: new object[,]
                {
                    { new Guid("5abf6839-2d26-4828-bbd8-e00b1cf37c0a"), new DateTime(2020, 10, 6, 13, 6, 47, 175, DateTimeKind.Local).AddTicks(918), new Guid("f71f74d5-4f5a-4947-9550-9b1488d29544"), "Community for car lovers.", "Cars", "https://res.cloudinary.com/dgepkksyl/image/upload/v1590193947/user_pics/cars2_c5wyqk.jpg" },
                    { new Guid("3dda3403-b576-4fe0-8b77-85f33e85ab59"), new DateTime(2020, 10, 6, 13, 6, 47, 175, DateTimeKind.Local).AddTicks(4944), new Guid("f71f74d5-4f5a-4947-9550-9b1488d29544"), "Community footbal, basketall, tenis and other.", "Sport", "https://res.cloudinary.com/dgepkksyl/image/upload/v1590193942/user_pics/sport2_fs0yab.jpg" },
                    { new Guid("fe153742-9a55-4475-8b11-a717cc19e7de"), new DateTime(2020, 10, 6, 13, 6, 47, 175, DateTimeKind.Local).AddTicks(5004), new Guid("f71f74d5-4f5a-4947-9550-9b1488d29544"), "Community for those who love cooking.", "Food", "https://res.cloudinary.com/dgepkksyl/image/upload/v1590193991/user_pics/food2_uckrcb.jpg" },
                    { new Guid("10232a6a-7180-46b8-8619-650be1e27e9e"), new DateTime(2020, 10, 6, 13, 6, 47, 175, DateTimeKind.Local).AddTicks(5013), new Guid("f71f74d5-4f5a-4947-9550-9b1488d29544"), "IT industry communicates here.", "Programming", "https://res.cloudinary.com/dgepkksyl/image/upload/v1590193952/user_pics/programing_wlmjj4.jpg" },
                    { new Guid("b66c829b-9736-4a14-adb3-80668b0a4c65"), new DateTime(2020, 10, 6, 13, 6, 47, 175, DateTimeKind.Local).AddTicks(5033), new Guid("f71f74d5-4f5a-4947-9550-9b1488d29544"), "Culture life.", "Culture", "https://res.cloudinary.com/dgepkksyl/image/upload/v1590193976/user_pics/culture2_hdclpb.jpg" }
                });

            migrationBuilder.InsertData(
                table: "UserSubscriptions",
                columns: new[] { "Id", "SubscriberUserId", "SubscriptionUserId" },
                values: new object[] { new Guid("2ec9827a-7e04-4d9c-a473-3d5103965cbf"), new Guid("f71f74d5-4f5a-4947-9550-9b1488d29544"), new Guid("61175164-ed74-4605-bf09-d22fcb4d72e7") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Communities",
                keyColumn: "Id",
                keyValue: new Guid("10232a6a-7180-46b8-8619-650be1e27e9e"));

            migrationBuilder.DeleteData(
                table: "Communities",
                keyColumn: "Id",
                keyValue: new Guid("3dda3403-b576-4fe0-8b77-85f33e85ab59"));

            migrationBuilder.DeleteData(
                table: "Communities",
                keyColumn: "Id",
                keyValue: new Guid("5abf6839-2d26-4828-bbd8-e00b1cf37c0a"));

            migrationBuilder.DeleteData(
                table: "Communities",
                keyColumn: "Id",
                keyValue: new Guid("b66c829b-9736-4a14-adb3-80668b0a4c65"));

            migrationBuilder.DeleteData(
                table: "Communities",
                keyColumn: "Id",
                keyValue: new Guid("fe153742-9a55-4475-8b11-a717cc19e7de"));

            migrationBuilder.DeleteData(
                table: "UserSubscriptions",
                keyColumn: "Id",
                keyValue: new Guid("2ec9827a-7e04-4d9c-a473-3d5103965cbf"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("61175164-ed74-4605-bf09-d22fcb4d72e7"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("f71f74d5-4f5a-4947-9550-9b1488d29544"));
        }
    }
}
