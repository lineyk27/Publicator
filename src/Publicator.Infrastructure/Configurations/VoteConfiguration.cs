using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Publicator.Infrastructure.Models;

namespace Publicator.Infrastructure.Configurations
{
    class VoteConfiguration : IEntityTypeConfiguration<Vote>
    {
        public void Configure(EntityTypeBuilder<Vote> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedOnAdd();
            builder.Property(e => e.CreationDate).HasColumnType("smalldatetime");
            builder
                .HasOne(e => e.User)
                .WithMany(e => e.Votes)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.NoAction);
            builder
                .HasOne(e => e.Post)
                .WithMany(e => e.Votes)
                .HasForeignKey(e => e.PostId);
        }
    }
}
