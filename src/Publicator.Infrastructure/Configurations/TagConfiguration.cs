using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Publicator.Infrastructure.Entities;

namespace Publicator.Infrastructure.Configurations
{
    class TagConfiguration : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedOnAdd();
            builder.Property(e => e.Name).IsUnicode();
            builder.HasMany(e => e.PostTags).WithOne(e => e.Tag).HasForeignKey(e => e.TagId);
            builder.HasMany(e => e.UserTags).WithOne(e => e.Tag).HasForeignKey(e => e.TagId);
        }
    }
}
