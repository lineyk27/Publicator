using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Publicator.Infrastructure.Models;

namespace Publicator.Infrastructure.Configurations
{
    class StateConfiguration : IEntityTypeConfiguration<State>
    {
        public void Configure(EntityTypeBuilder<State> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedOnAdd();
            builder.Property(e => e.Name).HasMaxLength(32);
            builder.HasMany(e => e.Users).WithOne(e => e.State).HasForeignKey(e => e.StateId);
        }
    }
}
