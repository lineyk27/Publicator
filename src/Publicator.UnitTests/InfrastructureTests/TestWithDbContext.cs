using System;
using Microsoft.EntityFrameworkCore;
using Publicator.Infrastructure;

namespace Publicator.UnitTests.InfrastructureTests
{
    public class TestWithDbContext : IDisposable
    {
        protected readonly PublicatorDbContext dbContext;
        public TestWithDbContext()
        {
            string connString = "Server=DESKTOP-I4GI6P8\\SQLEXPRESS;" +
                "Database=PublicatorTest;" +
                "Trusted_Connection=True;" +
                "MultipleActiveResultSets=true";

            var optionsBuilder = new DbContextOptionsBuilder<PublicatorDbContext>().UseSqlServer(connString);

            dbContext = new PublicatorDbContext(optionsBuilder.Options);
            dbContext.Database.EnsureCreated();
        }

        public void Dispose()
        {
            dbContext.Dispose();
        }
    }
}
