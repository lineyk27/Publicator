using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Publicator.Infrastructure.Entities;
using Publicator.Infrastructure.Configurations;

namespace Publicator.Infrastructure
{
    public class PublicatorDbContext : DbContext
    {
        public PublicatorDbContext() : base()
        {}
        public PublicatorDbContext(DbContextOptions<PublicatorDbContext> options) : base(options)
        {}
        public DbSet<Post> Posts { get; set; }
        public DbSet<Bookmark> Bookmarks { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<PostTag> PostTags { get; set; }
        public DbSet<UserTag> UserTags{ get; set; }
        public DbSet<SubscriptionNewPost> SubscriptionNewPosts { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<UserSubscription> UserSubscriptions { get; set; }
        public DbSet<Vote> Votes { get; set; }
        public DbSet<UserCommunity> UserCommunities { get; set; }
        public DbSet<Community> Communities { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new PostConfiguration());
            builder.ApplyConfiguration(new BookmarkConfiguration());
            builder.ApplyConfiguration(new CommentConfiguration());
            builder.ApplyConfiguration(new PostTagConfiguration());
            builder.ApplyConfiguration(new SubscriptionNewPostConfiguration());
            builder.ApplyConfiguration(new TagConfiguration());
            builder.ApplyConfiguration(new UserSubscriptionConfiguration());
            builder.ApplyConfiguration(new VoteConfiguration());
            builder.ApplyConfiguration(new UserCommunityConfiguration());
            builder.ApplyConfiguration(new CommunityConfiguration());
            builder.ApplyConfiguration(new RoleConfiguration());
            builder.ApplyConfiguration(new UserTagConfiguration());
            builder.ApplyConfiguration(new StateConfiguration());
        }
    }
}
