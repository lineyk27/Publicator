using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Publicator.Infrastructure.Models;
using Publicator.Infrastructure.Configurations;
using Microsoft.AspNetCore.Identity;
using System;

namespace Publicator.Infrastructure
{
    public class PublicatorDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
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
        public DbSet<UserSubscription> UserSubscriptions { get; set; }
        public DbSet<Vote> Votes { get; set; }
        public DbSet<UserCommunity> UserCommunities { get; set; }
        public DbSet<Community> Communities { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            Seed.Seed.AddData(builder);

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
            builder.ApplyConfiguration(new UserTagConfiguration());
        }
        public override void Dispose()
        {
            SaveChanges();
            base.Dispose();
        }
        public override ValueTask DisposeAsync()
        {
            SaveChangesAsync();
            return base.DisposeAsync();
        }
    }
}
