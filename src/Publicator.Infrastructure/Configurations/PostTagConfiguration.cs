using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Publicator.Infrastructure.Models;

namespace Publicator.Infrastructure.Configurations
{
    class PostTagConfiguration : IEntityTypeConfiguration<PostTag>
    {
        public void Configure(EntityTypeBuilder<PostTag> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedOnAdd();
            builder.HasOne(e => e.Post).WithMany(e => e.PostTags).HasForeignKey(e => e.PostId);
            builder.HasOne(e => e.Tag).WithMany(e => e.PostTags).HasForeignKey(e => e.TagId);
        }
    }
}
