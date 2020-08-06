using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Publicator.Infrastructure.Models;

namespace Publicator.Infrastructure.Configurations
{
    class CommunityConfiguration : IEntityTypeConfiguration<Community>
    {
        public void Configure(EntityTypeBuilder<Community> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedOnAdd();
            builder.Property(e => e.Name).HasMaxLength(64).IsUnicode();
            builder.Property(e => e.PictureName).HasMaxLength(512);
            builder.Property(e => e.Description).HasMaxLength(256);
            builder
                .HasOne(e => e.CreatorUser)
                .WithMany(e => e.CreatedCommunities)
                .HasForeignKey(e => e.CreatorUserId);
            builder
                .HasMany(e => e.UserCommunities)
                .WithOne(e => e.Community)
                .HasForeignKey(e => e.CommunityId);
            builder
                .HasMany(e => e.SubscriptionNewPosts)
                .WithOne(e => e.SubscriptionCommunity)
                .HasForeignKey(e => e.SubscriptionCommunityId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
