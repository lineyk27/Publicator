using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Publicator.Infrastructure.Entities;

namespace Publicator.Infrastructure.Configurations
{
    class UserSubscriptionConfiguration : IEntityTypeConfiguration<UserSubscription>
    {
        public void Configure(EntityTypeBuilder<UserSubscription> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedOnAdd();
            builder
                .HasOne(e => e.SubscriberUser)
                .WithMany(e => e.Subscriptions)
                .HasForeignKey(e => e.SubscriberUserId)
                .OnDelete(DeleteBehavior.Cascade);
            builder
                .HasOne(e => e.SubscriptionUser)
                .WithMany(e => e.Subscribers)
                .HasForeignKey(e => e.SubscriptionUserId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
