using MediatR;
using Microsoft.AspNetCore.Mvc;
using Redirect.API.Extensions;
using Redirect.Application.Commands.GenerateNewShortenedUrl;
using Redirect.Application.Queries.GetOriginalUrlByCode;

namespace Redirect.API.Controllers
{
    [Route("")]
    public class UrlShortenerController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UrlShortenerController(IMediator mediator, IHttpContextAccessor httpContextAccessor)
        {
            _mediator = mediator;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Generates a new shortened url code for the original informed url
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("generate-new-shortened-url")]
        public async Task<IActionResult> GenerateNewShortenedUrlAsync([FromBody] GenerateNewShortenedUrlCommand command)
        {
            var newShortenedUrlCode = await _mediator.Send(command);

            var baseUrl = _httpContextAccessor.HttpContext?.Request.BaseUrl() ?? "";

            return Ok(new { shortenedUrl = $"{baseUrl}{newShortenedUrlCode}" });
        }

        /// <summary>
        /// Redirects to the original url from the entered short url code, if the code does not exist in the database, 404 is returned.
        /// </summary>
        /// <param name="code">Shortened url code used to fetch the original url</param>
        /// <returns></returns>
        [HttpGet("{code}")]
        public async Task<ActionResult> GetOriginalUrlByCodeAsync(string code)
        {
            var getOriginalUrlByCodeQuery = new GetOriginalUrlByCodeQuery { Code = code };

            var originalUrl = await _mediator.Send(getOriginalUrlByCodeQuery);

            if (originalUrl == null)
                return NotFound("Não foi encontrado nenhum endereço com o código informado.");
            
            return new RedirectResult(originalUrl);
        }
    }
}