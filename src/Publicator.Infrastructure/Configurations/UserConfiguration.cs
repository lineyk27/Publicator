using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Publicator.Infrastructure.Models;

namespace Publicator.Infrastructure.Configurations
{
    class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedOnAdd();
            builder.Property(e => e.JoinDate).HasColumnType("smalldatetime");
            builder.Property(e => e.PictureUrl).IsUnicode().HasMaxLength(256);
            builder
                .HasMany(e => e.Subscriptions)
                .WithOne(e => e.SubscriberUser)
                .HasForeignKey(e => e.SubscriberUserId)
                .OnDelete(DeleteBehavior.NoAction);
            builder
                .HasMany(e => e.Subscribers)
                .WithOne(e => e.SubscriptionUser)
                .HasForeignKey(e => e.SubscriptionUserId)
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(e => e.Votes).WithOne(e => e.User).HasForeignKey(e => e.UserId);
            builder.HasMany(e => e.Comments).WithOne(e => e.User).HasForeignKey(e => e.UserId);
            builder
                .HasMany(e => e.Posts)
                .WithOne(e => e.CreatorUser)
                .HasForeignKey(e => e.CreatorUserId)
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(e => e.SubscribeTags).WithOne(e => e.User).HasForeignKey(e => e.UserId);
            builder
                .HasMany(e => e.SubscriptionNewPosts)
                .WithOne(e => e.User)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(e => e.Bookmarks).WithOne(e => e.User).HasForeignKey(e => e.UserId);
            builder.HasMany(e => e.UserCommunities).WithOne(e => e.User).HasForeignKey(e => e.UserId);
        }
    }
}
