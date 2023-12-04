

using ErrorOr;
using MediatR;
using Organisation.Application.Common.DTO.External;
using Organisation.Application.ThirdPartyServices;

namespace Organisation.Application.ProductModule.Queries.GetProducts;

public sealed class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, ErrorOr<List<ProductResponse>>>
{
    private readonly ExternalProductsService _externalProductService;
    public GetProductsQueryHandler(ExternalProductsService externalProductService)
    {
        _externalProductService = externalProductService;
    }
    public async Task<ErrorOr<List<ProductResponse>>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        return await _externalProductService.GetExternalProductsAsync();
    }
}
