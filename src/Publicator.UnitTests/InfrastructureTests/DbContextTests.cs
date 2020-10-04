using Xunit;

namespace Publicator.UnitTests.InfrastructureTests
{
    public class DbContextTests : TestWithDbContext
    {
        [Fact]
        public void DatabaseIsCreated()
        {
            Assert.True(dbContext.Database.CanConnect());
        }
    }
}
