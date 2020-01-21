using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Publicator.Infrastructure.Entities;

namespace Publicator.Infrastructure.Configurations
{
    class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedOnAdd();
            builder.Property(e => e.Email).IsUnicode().HasMaxLength(128);
            builder.Property(e => e.Nickname).IsUnicode().HasMaxLength(64);
            builder.Property(e => e.JoinDate).HasColumnType("smalldatetime");
            builder.Property(e => e.PasswordHash).IsUnicode().HasMaxLength(64);
            builder.Property(e => e.EmailConfirmed).HasDefaultValue(false);
            builder.Property(e => e.PictureName).IsUnicode().HasMaxLength(64);
            builder.HasOne(e => e.State).WithMany(e => e.Users).HasForeignKey(e => e.StateId);
            builder.HasOne(e => e.Role).WithMany(e => e.Users).HasForeignKey(e => e.RoleId);
            builder.HasMany(e => e.Subscriptions).WithOne(e => e.SubscriberUser).HasForeignKey(e => e.SubscriberUserId);
            builder.HasMany(e => e.Subscribers).WithOne(e => e.SubscriptionUser).HasForeignKey(e => e.SubscriptionUserId);
            builder.HasMany(e => e.Votes).WithOne(e => e.User).HasForeignKey(e => e.UserId);
            builder.HasMany(e => e.Comments).WithOne(e => e.User).HasForeignKey(e => e.UserId);
            builder.HasMany(e => e.Posts).WithOne(e => e.CreatorUser).HasForeignKey(e => e.CreatorUserId);
            builder.HasMany(e => e.SubscribeTags).WithOne(e => e.User).HasForeignKey(e => e.UserId);
            builder.HasMany(e => e.SubscriptionNewPosts).WithOne(e => e.User).HasForeignKey(e => e.UserId);
            builder.HasMany(e => e.Bookmarks).WithOne(e => e.User).HasForeignKey(e => e.UserId);
            builder.HasMany(e => e.UserCommunities).WithOne(e => e.User).HasForeignKey(e => e.UserId);
        }
    }
}
