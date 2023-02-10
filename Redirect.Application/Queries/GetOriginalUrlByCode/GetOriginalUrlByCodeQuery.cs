using MediatR;

namespace Redirect.Application.Queries.GetOriginalUrlByCode
{
    public class GetOriginalUrlByCodeQuery : IRequest<string>
    {
        public string Code { get; set; }
    }
}