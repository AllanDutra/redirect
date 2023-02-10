using Redirect.Core.Entities;

namespace Redirect.Core.Repositories
{
    public interface IRedirectRepository
    {
        public Task RegisterNewShortenedUrlAsync(ShortenedUrl shortenedUrl);
        public Task<string> GetOriginalUrlByCodeAsync(string code);
        public Task DeleteExpiredsShortenedsUrlAsync();
    }
}