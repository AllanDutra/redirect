using MediatR;
using Redirect.Core.Entities;
using Redirect.Core.Repositories;

namespace Redirect.Application.Commands.GenerateNewShortenedUrl
{
    public class GenerateNewShortenedUrlCommandHandler : IRequestHandler<GenerateNewShortenedUrlCommand, string>
    {
        private const int SHORTENED_URL_EXPIRATION_IN_DAYS = 7;
        private readonly IRedirectRepository _redirectRepository;
        public GenerateNewShortenedUrlCommandHandler(IRedirectRepository redirectRepository)
        {
            _redirectRepository = redirectRepository;
        }

        public async Task<string> Handle(GenerateNewShortenedUrlCommand request, CancellationToken cancellationToken)
        {
            var code = this.RandomString(8);

            var newShortenedUrl = new ShortenedUrl(code, request.OriginalUrl, DateTime.Now.AddDays(SHORTENED_URL_EXPIRATION_IN_DAYS));

            await _redirectRepository.RegisterNewShortenedUrlAsync(newShortenedUrl);

            return code;
        }

        private string RandomString(int length)
        {
            Random random = new Random();

            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}