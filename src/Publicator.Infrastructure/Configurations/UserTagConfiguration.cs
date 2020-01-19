using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Publicator.Infrastructure.Entities;

namespace Publicator.Infrastructure.Configurations
{
    class UserTagConfiguration : IEntityTypeConfiguration<UserTag>
    {
        public void Configure(EntityTypeBuilder<UserTag> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedOnAdd();
            builder.HasOne(e => e.Tag).WithMany(e => e.SubscribeTags).HasForeignKey(e => e.TagId);
            builder.HasOne(e => e.User).WithMany(e => e.SubscribeTags).HasForeignKey(e => e.UserId);
        }
    }
}
