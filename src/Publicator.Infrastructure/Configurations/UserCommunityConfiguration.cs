using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Publicator.Infrastructure.Entities;

namespace Publicator.Infrastructure.Configurations
{
    class UserCommunityConfiguration : IEntityTypeConfiguration<UserCommunity>
    {
        public void Configure(EntityTypeBuilder<UserCommunity> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedOnAdd();
            builder
                .HasOne(e => e.User)
                .WithMany(e => e.UserCommunities)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.NoAction);
            builder
                .HasOne(e => e.Community)
                .WithMany(e => e.UserCommunities)
                .HasForeignKey(e => e.CommunityId)
                .OnDelete(DeleteBehavior.NoAction);
            builder.Property(e => e.CreationDate).HasColumnType("smalldatetime");
        }
    }
}
