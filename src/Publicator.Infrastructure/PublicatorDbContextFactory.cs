using Microsoft.EntityFrameworkCore;

namespace Publicator.Infrastructure
{
    public static class PublicatorDbContextFactory
    {
        public static PublicatorDbContext Create(string connectionstring)
        {
            var build = new DbContextOptionsBuilder<PublicatorDbContext>();
            build.UseSqlServer(connectionstring);
            return new PublicatorDbContext(build.Options);
        }
    }
}


