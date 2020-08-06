using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Publicator.Infrastructure.Models;

namespace Publicator.Infrastructure.Configurations
{
    class SubscriptionNewPostConfiguration : IEntityTypeConfiguration<SubscriptionNewPost>
    {
        public void Configure(EntityTypeBuilder<SubscriptionNewPost> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedOnAdd();
            builder
                .HasOne(e => e.User)
                .WithMany(e => e.SubscriptionNewPosts)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.NoAction);
            builder
                .HasOne(e => e.Post)
                .WithMany(e => e.SubscriptionNewPosts)
                .HasForeignKey(e => e.PostId);
            builder
                .HasOne(e => e.SubscriptionCommunity)
                .WithMany(e => e.SubscriptionNewPosts)
                .HasForeignKey(e => e.SubscriptionCommunityId);
        }
    }
}
