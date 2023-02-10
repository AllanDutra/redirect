using MediatR;
using Redirect.Core.Repositories;

namespace Redirect.Application.Queries.GetOriginalUrlByCode
{
    public class GetOriginalUrlByCodeQueryHandler : IRequestHandler<GetOriginalUrlByCodeQuery, string>
    {
        private readonly IRedirectRepository _redirectRepository;
        public GetOriginalUrlByCodeQueryHandler(IRedirectRepository redirectRepository)
        {
            _redirectRepository = redirectRepository;
        }

        public async Task<string> Handle(GetOriginalUrlByCodeQuery request, CancellationToken cancellationToken)
        {
            var originalUrl = await _redirectRepository.GetOriginalUrlByCodeAsync(request.Code);
            
            return originalUrl;
        }
    }
}