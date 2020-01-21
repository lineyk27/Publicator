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
            // seed tags
            var tag1 = new Tag() { Id = Guid.NewGuid(), Name = "Life" };
            var tag2 = new Tag() { Id = Guid.NewGuid(), Name = "Motorcycle" };
            var tag3 = new Tag() { Id = Guid.NewGuid(), Name = "Car" };
            var tag4 = new Tag() { Id = Guid.NewGuid(), Name = "Politic" };
            // seed users
            var user1 = new User()
            {
                Id = Guid.NewGuid(),
                Email = "lineyk27gg@gmail.com",
                Nickname = "lineyk27gg",
                // decode - 12345678
                PasswordHash = "ef797c8118f02dfb649607dd5d3f8c7623048c9c063d532cc95c5ed7a898a64f", // using plain text, only for tests
                JoinDate = DateTime.Now,
                BeginStateDate = DateTime.Now,
                EndStateDate = new DateTime(2050, 12, 12),
                StateId = stateActive.Id,
                RoleId = roleSimple.Id
            };
            var user2 = new User()
            {
                Id = Guid.NewGuid(),
                Nickname = "lineyk27",
                Email = "lineyk27@yandex.ru",
                // decode - password
                PasswordHash = "5e884898da28047151d0e56f8dc6292773603d0d6aabbdd62a11ef721d1542d8", 
                JoinDate = DateTime.Now,
                BeginStateDate = DateTime.Now,
                EndStateDate = new DateTime(2050, 12, 13),
                StateId = stateActive.Id,
                RoleId = roleSimple.Id
            };
            // seed communities
            var community1 = new Community()
            {
                Id = Guid.NewGuid(),
                Name = "Stories about Life",
                Description = "Here users post stories about theirs lifes.",
                CreationDate = DateTime.Now,
                CreatorUserId = user1.Id
            };
            var community2 = new Community()
            {
                Id = Guid.NewGuid(),
                Name = "Vehicle",
                Description = "Community about vehicles and all about it.",
                CreationDate = DateTime.Now,
                CreatorUserId = user1.Id
            };
            // seed posts
            var post1 = new Post()
            {
                Id = Guid.NewGuid(),
                Name = "Post about my life",
                Content = "<p>In this post i want to tell you story of my life...</p>",
                CreationDate = DateTime.Now,
                CreatorUserId = user1.Id,
                CommunityId = community1.Id
            };
            var post2 = new Post()
            {
                Id = Guid.NewGuid(),
                Name = "Happened in Sokal",
                Content = "<p>Once upon a time in Ukraine, village Sokal i studied in school...</p>",
                CreationDate = DateTime.Now,
                CreatorUserId = user2.Id,
                CommunityId = community1.Id
            };
            // seed comments
            var comment1 = new Comment()
            {
                Id = Guid.NewGuid(),
                UserId = user1.Id,
                PostId = post1.Id,
                CreationDate = DateTime.Now.AddDays(1),
                Content = "<p>Hilarious, how it happened with you</p>"
            };
            var comment2 = new Comment()
            {
                Id = Guid.NewGuid(),
                UserId = user2.Id,
                PostId = post1.Id,
                CreationDate = DateTime.Now.AddDays(2),
                Content = "<p>It happened in 2016 year, i was 16 years old</p>",
                ParentRepliedCommentId = comment1.Id
            };
            // seed bookmarks
            var bookmark1 = new Bookmark()
            {
                Id = Guid.NewGuid(),
                CreationDate = DateTime.Now.AddDays(3),
                UserId = user1.Id,
                PostId = post2.Id
            };
            // seed post tags
            var postTag1 = new PostTag() { Id = Guid.NewGuid(), PostId = post1.Id, TagId = tag1.Id };
            var postTag2 = new PostTag() { Id = Guid.NewGuid(), PostId = post1.Id, TagId = tag4.Id };
            // seed subscriptions
            var subscription1 = new UserSubscription() {
                Id = Guid.NewGuid(),
                SubscriberUserId = user1.Id, 
                SubscriptionUserId = user2.Id
            };
            builder.Entity<Tag>().HasData(tag1, tag2, tag3, tag4);
            builder.Entity<User>().HasData(user1, user2);
            builder.Entity<Community>().HasData(community1, community2);
            builder.Entity<Post>().HasData(post1, post2);
            builder.Entity<UserSubscription>().HasData(subscription1);
            builder.Entity<Comment>().HasData(comment1, comment2);
            builder.Entity<PostTag>().HasData(postTag1, postTag2);
            builder.Entity<State>().HasData(stateActive, stateFreezed);
            builder.Entity<Role>().HasData(roleAdmin, roleModerator, roleSimple);
            builder.Entity<Bookmark>().HasData(bookmark1);
        }
    }
}
