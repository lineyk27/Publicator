using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Publicator.Infrastructure.Models;

namespace Publicator.Infrastructure.Configurations
{
    class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedOnAdd();
            builder.Property(e => e.Content).IsUnicode();
            builder
                .Property(e => e.CreationDate)
                .HasColumnType("smalldatetime");
            builder
                .HasOne(e => e.User)
                .WithMany(e => e.Comments)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.NoAction);
            builder
                .HasOne(e => e.Post)
                .WithMany(e => e.Comments)
                .HasForeignKey(e => e.PostId);
            builder
                .HasOne(e => e.ParentRepliedComment)
                .WithMany(e => e.RepliesComments)
                .HasForeignKey(e => e.ParentRepliedCommentId);
        }
    }
}
