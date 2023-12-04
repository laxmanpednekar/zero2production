using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Organisation.Application.Common.DTO;
using Organisation.Application.Common.Interfaces.Persistance;
using Organisation.Application.Common.Utilities;
using Organisation.Application.CompanyModule.Commands.AddCompany;
using Organisation.Application.CompanyModule.Commands.DeleteCompany;
using Organisation.Application.CompanyModule.Commands.UpdateCompany;
using Organisation.Application.CompanyModule.Queries.GetCompanies;
using Organisation.Application.CompanyModule.Queries.GetCompanyById;
using Organisation.Domain.Company;
using Organisation.Domain.Common.Enums;
using Organisation.Infrastructure.Common.Utilities;

namespace Organisation.Presentation.API.Controllers.V1;

[ApiController]
[Route("api/v{v:apiVersion}/[controller]")]
[ApiVersion("1.0")]
[Produces("application/json")]
public sealed class CompaniesController : BaseAPIController
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public CompaniesController(ISender sender, IMapper mapper)
    {
        _sender = sender;
        _mapper = mapper;
    }

    /// <summary>
    /// This endpoint gets all the companies in the system.
    /// </summary>
    /// <respone code="200">Returns paged list of all companies in the system</respone>
    [HttpGet]
    [ProducesResponseType(typeof(PageList<CompanyResponse>),200)]
    [HasPermission(Permission.List)]
    public async Task<IActionResult> GetCompanies([FromQuery] CompanyQueryParameters queryParameters)
    {
        return Ok(await _sender.Send(new GetCompaniesQuery(queryParameters)));
    }

    /// <summary>
    /// This endpoint gets a particular company from the system based on the provided copany id.
    /// </summary>
    /// <param name="id">**string**</param>
    /// <response code="200">Gets a comapny successfully.</response>
    /// <response code="404">Could not find the company.</response>
    /// <returns>Company</returns>
    [HttpGet("company/{id:length(22)}")]
    public async Task<IActionResult> GetCompanyById(string id)
    {
        var result = await _sender.Send(new GetCompanyByIdQuery(id));
        return result.Match(
               r => Ok(r),
               errors => Problem(errors)
        );
 
    }

    /// <summary>
    /// This creates a company in the system.
    /// </summary>
    /// <param name="companyRequest">**CompanyRequest**</param>
    /// <response code="201">Creates a company successfullly</response>
    [HttpPost("company")]
    public async Task<IActionResult> AddCompany(CompanyRequest companyRequest)
    {
        var addCompanyCommand = _mapper.Map<AddCompanyCommand>(companyRequest); //new AddCompanyCommand(companyRequest.Name, companyRequest.Address, companyRequest.Country);
        var companyId = await _sender.Send(addCompanyCommand);

        return CreatedAtAction("GetCompanyById", new { id = companyId }, companyRequest);
    }

    [HttpPut("company/{id:length(22)}")]
    public async Task<IActionResult> UpdateCompany(string id, [FromBody] CompanyRequest companyRequest)
    {
        var updateCompanyCommand = _mapper.Map<UpdateCompanyCommand>((id, companyRequest));//new UpdateCompanyCommand(id, companyRequest.Name, companyRequest.Address, companyRequest.Country);
        var result = await _sender.Send(updateCompanyCommand);

        return result.Match(
            r => NoContent(),
            errors => Problem(errors)
        );
    }

    [HttpDelete("company/{id:length(22)}")]
    public async Task<IActionResult> DeleteCompany(string id, [FromBody] bool deleteAssociations)
    {
        var deleteCompanyCommand = new DeleteCompanyCommand(id, deleteAssociations);
        await _sender.Send(deleteCompanyCommand);

        return NoContent();
    }
}
