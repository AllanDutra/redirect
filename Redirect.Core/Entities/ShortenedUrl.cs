namespace Redirect.Core.Entities;

public partial class ShortenedUrl
{
    public ShortenedUrl(string code, string originalUrl, DateTime expiration)
    {
        Code = code;
        OriginalUrl = originalUrl;
        Expiration = expiration;
    }

    public string Code { get; private set; }

    public string OriginalUrl { get; private set; }

    public DateTime Expiration { get; private set; }
}
