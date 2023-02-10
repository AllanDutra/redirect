using MediatR;

namespace Redirect.Application.Commands.GenerateNewShortenedUrl
{
    public class GenerateNewShortenedUrlCommand : IRequest<string>
    {
        public string OriginalUrl { get; set; }
    }
}