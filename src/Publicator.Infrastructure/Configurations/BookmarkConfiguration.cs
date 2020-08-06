using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Publicator.Infrastructure.Models;

namespace Publicator.Infrastructure.Configurations
{
    class BookmarkConfiguration : IEntityTypeConfiguration<Bookmark>
    {
        public void Configure(EntityTypeBuilder<Bookmark> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedOnAdd();
            builder
                .Property(e => e.CreationDate)
                .HasColumnType("smalldatetime");
            builder
                .HasOne(e => e.Post)
                .WithMany(e => e.Bookmarks)
                .HasForeignKey(e => e.PostId);
            builder
                .HasOne(e => e.User)
                .WithMany(e => e.Bookmarks)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
