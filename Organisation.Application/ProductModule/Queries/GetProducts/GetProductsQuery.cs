

using ErrorOr;
using MediatR;
using Organisation.Application.Common.DTO.External;

namespace Organisation.Application.ProductModule.Queries.GetProducts;

public record GetProductsQuery() : IRequest<ErrorOr<List<ProductResponse>>>;
