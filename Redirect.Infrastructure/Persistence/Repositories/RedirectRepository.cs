using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql;
using Redirect.Core.Entities;
using Redirect.Core.Repositories;

namespace Redirect.Infrastructure.Persistence.Repositories
{
    public class RedirectRepository : IRedirectRepository
    {
        private readonly RedirectDbContext _dbContext;
        private readonly string _connectionString;
        public RedirectRepository(RedirectDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _connectionString = configuration.GetConnectionString("RedirectCs");
        }

        public async Task DeleteExpiredsShortenedsUrlAsync()
        {
            using (var sqlConnection = new NpgsqlConnection(_connectionString))
            {
                sqlConnection.Open();

                var script = "DELETE FROM \"shortenedURLs\" SU WHERE SU.\"Expiration\" <= @now";

                await sqlConnection.ExecuteAsync(script, new { now = DateTime.Now });
            }
        }

        public async Task<string> GetOriginalUrlByCodeAsync(string code)
        {
            var selectedShortenedUrl = await _dbContext.ShortenedUrls.Select(su => new { su.Code, su.OriginalUrl }).FirstOrDefaultAsync(su => su.Code == code);

            if (selectedShortenedUrl == null)
                return null;

            return selectedShortenedUrl.OriginalUrl;
        }

        public async Task RegisterNewShortenedUrlAsync(ShortenedUrl shortenedUrl)
        {
            await _dbContext.ShortenedUrls.AddAsync(shortenedUrl);

            await _dbContext.SaveChangesAsync();
        }
    }
}