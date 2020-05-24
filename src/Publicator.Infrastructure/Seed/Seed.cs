using System;
using Microsoft.EntityFrameworkCore;
using Publicator.Infrastructure.Entities;

namespace Publicator.Infrastructure.Seed
{
    public static class Seed
    {
        public static void AddData(ModelBuilder builder)
        {
            // seed user roles
            var roleAdmin = new Role() { Id = Guid.NewGuid(), Name = "Administrator" };
            var roleModerator = new Role() { Id = Guid.NewGuid(), Name = "Moderator" };
            var roleSimple = new Role() { Id = Guid.NewGuid(), Name = "Simple" };
            // seed user states
            var stateActive = new State() { Id = Guid.NewGuid(), Name = "Active" };
            var stateFreezed = new State() { Id = Guid.NewGuid(), Name = "Freezed" };
            // seed users
            var user1 = new User()
            {
                Id = Guid.NewGuid(),
                Email = "lineyk27gg@gmail.com",
                Nickname = "lineyk27",
                // decode - 12345678
                PasswordHash = "ef797c8118f02dfb649607dd5d3f8c7623048c9c063d532cc95c5ed7a898a64f",
                JoinDate = DateTime.Now,
                BeginStateDate = DateTime.Now,
                EndStateDate = new DateTime(2050, 12, 12),
                StateId = stateActive.Id,
                RoleId = roleSimple.Id,
                PictureName = "https://res.cloudinary.com/dgepkksyl/image/upload/v1590192208/user_pics/IMG_0572_ut5cxl.jpg"
            };
            var user2 = new User()
            {
                Id = Guid.NewGuid(),
                Nickname = "kit22",
                Email = "lineyk27@gmail.com",
                // decode - password
                PasswordHash = "5e884898da28047151d0e56f8dc6292773603d0d6aabbdd62a11ef721d1542d8", 
                JoinDate = DateTime.Now,
                BeginStateDate = DateTime.Now,
                EndStateDate = new DateTime(2050, 12, 13),
                StateId = stateActive.Id,
                RoleId = roleSimple.Id,
                PictureName = "https://res.cloudinary.com/dgepkksyl/image/upload/v1590194554/user_pics/index_gz1sqk.jpg"
            };
            // seed communities
            var community1 = new Community()
            {
                Id = Guid.NewGuid(),
                Name = "Cars",
                Description = "Community for car lovers.",
                CreationDate = DateTime.Now,
                CreatorUserId = user1.Id,
                PictureName = "https://res.cloudinary.com/dgepkksyl/image/upload/v1590193947/user_pics/cars2_c5wyqk.jpg"
            };
            var community2 = new Community()
            {
                Id = Guid.NewGuid(),
                Name = "Sport",
                Description = "Community footbal, basketall, tenis and other.",
                CreationDate = DateTime.Now,
                CreatorUserId = user1.Id,
                PictureName = "https://res.cloudinary.com/dgepkksyl/image/upload/v1590193942/user_pics/sport2_fs0yab.jpg"
            };
            
            var community3 = new Community()
            {
                Id = Guid.NewGuid(),
                Name = "Food",
                Description = "Community for those who love cooking.",
                CreationDate = DateTime.Now,
                CreatorUserId = user1.Id,
                PictureName = "https://res.cloudinary.com/dgepkksyl/image/upload/v1590193991/user_pics/food2_uckrcb.jpg"
            };

            var community4 = new Community()
            {
                Id = Guid.NewGuid(),
                Name = "Programming",
                Description = "IT industry communicates here.",
                CreationDate = DateTime.Now,
                CreatorUserId = user1.Id,
                PictureName = "https://res.cloudinary.com/dgepkksyl/image/upload/v1590193952/user_pics/programing_wlmjj4.jpg"
            };

            var community5 = new Community()
            {
                Id = Guid.NewGuid(),
                Name = "Culture",
                Description = "Culture life.",
                CreationDate = DateTime.Now,
                CreatorUserId = user1.Id,
                PictureName = "https://res.cloudinary.com/dgepkksyl/image/upload/v1590193976/user_pics/culture2_hdclpb.jpg"
            };
            // seed subscriptions
            var subscription1 = new UserSubscription() {
                Id = Guid.NewGuid(),
                SubscriberUserId = user1.Id, 
                SubscriptionUserId = user2.Id
            };
            builder.Entity<User>().HasData(user1, user2);
            builder.Entity<Community>().HasData(community1, community2, community3, community4, community5);
            builder.Entity<UserSubscription>().HasData(subscription1);
            builder.Entity<State>().HasData(stateActive, stateFreezed);
            builder.Entity<Role>().HasData(roleAdmin, roleModerator, roleSimple);
        }
    }
}
