using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Organisation.Application.Common.ApplicationConfigOptions;
using Organisation.Application.Common.DTO.External;
using Organisation.Application.ProductModule.Queries.GetProducts;

namespace Organisation.Presentation.API.Controllers.V1;

[ApiController]
[Route("api/v{v:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public sealed class ProductsController :BaseAPIController
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public ProductsController(ISender sender, IMapper mapper)
    {
        _sender = sender;
        _mapper = mapper;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetProducts()
    {

        var result = await _sender.Send(new GetProductsQuery());

        return result.Match(
                   result => Ok(result),
                   errors => Problem(errors)
               );
    }

}
