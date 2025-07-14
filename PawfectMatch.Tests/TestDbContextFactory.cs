using Microsoft.EntityFrameworkCore;
using PawfectMatch.Data;

namespace PawfectMatch.Tests
{
    public class TestDbContextFactory : IDbContextFactory<ApplicationDbContext>
    {
        private readonly DbContextOptions<ApplicationDbContext> _options;
        public TestDbContextFactory(DbContextOptions<ApplicationDbContext> options) => _options = options;
        public ApplicationDbContext CreateDbContext() => new ApplicationDbContext(_options);
        public Task<ApplicationDbContext> CreateDbContextAsync(CancellationToken cancellationToken = default) => Task.FromResult(new ApplicationDbContext(_options));
    }
}