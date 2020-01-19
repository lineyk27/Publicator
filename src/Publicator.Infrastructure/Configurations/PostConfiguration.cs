using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Publicator.Infrastructure.Entities;

namespace Publicator.Infrastructure.Configurations
{
    class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedOnAdd();
            builder.Property(e => e.Name).IsUnicode().HasMaxLength(128);
            builder.Property(e => e.CreationDate).HasColumnType("smalldatetime");
            builder.Property(e => e.Content).HasColumnType("ntext");
            builder.Property(e => e.CurrentRating).HasDefaultValue(0);
            builder.HasOne(e => e.CreatorUser).WithMany(e => e.Posts).HasForeignKey(e => e.CreatorUserId);
            builder.HasMany(e => e.Votes).WithOne(e => e.Post).HasForeignKey(e => e.PostId);
            builder.HasMany(e => e.Comments).WithOne(e => e.Post).HasForeignKey(e => e.PostId);
            builder.HasMany(e => e.PostTags).WithOne(e => e.Post).HasForeignKey(e => e.PostId);
            builder.HasMany(e => e.Bookmarks).WithOne(e => e.Post).HasForeignKey(e => e.PostId);
            builder.HasOne(e => e.Community).WithMany(e => e.Posts).HasForeignKey(e => e.CommunityId);
        }
    }
}
