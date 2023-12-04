using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Organisation.Application.Common.ApplicationConfigOptions;
using Organisation.Application.Common.DTO;
using Organisation.Application.Common.Interfaces.Persistance;
using Organisation.Application.CompanyModule.Commands.AddCompany;
using Organisation.Application.CompanyModule.Commands.DeleteCompany;
using Organisation.Application.CompanyModule.Commands.UpdateCompany;
using Organisation.Application.CompanyModule.Queries.GetCompanies;
using Organisation.Application.CompanyModule.Queries.GetCompanyById;
using Organisation.Application.CompanyModule.Queries.GetCompanyCount;
using Organisation.Domain.Company;
using Organisation.Domain.Models;

namespace Organisation.Presentation.API.Controllers.V2;

[ApiController]
[Route("api/v{v:apiVersion}/[controller]")]
[ApiVersion("2.0")]
public sealed class CompaniesController : BaseAPIController
{

    private readonly ISender _sender;
    public CompaniesController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet]
    public async Task<IActionResult> GetCompanies([FromQuery] CompanyQueryParameters queryParameters)
    {
        return Ok(await _sender.Send(new GetCompaniesQuery(queryParameters)));
    }

    [HttpGet("company/{id:length(22)}")]
    public async Task<IActionResult> GetCompanyById(string id)
    {
        return Ok(await _sender.Send(new GetCompanyByIdQuery(id)));
    }

    [HttpPost("company")]
    public async Task<IActionResult> AddCompany(CompanyRequest companyRequest)
    {
        var addCompanyCommand = new AddCompanyCommand(companyRequest.Name, companyRequest.Address, companyRequest.Country);
        var companyId = await _sender.Send(addCompanyCommand);

        return CreatedAtAction("GetCompanyById", new { id = companyId }, companyRequest);
    }

    [HttpPut("company/{id:length(22)}")]
    public async Task<IActionResult> UpdateCompany(string id, [FromBody] CompanyRequest companyRequest)
    {
        var updateCompanyCommand = new UpdateCompanyCommand(id, companyRequest.Name, companyRequest.Address, companyRequest.Country);
        await _sender.Send(updateCompanyCommand);
        return NoContent();
    }

    [HttpDelete("company/{id:length(22)}")]
    public async Task<IActionResult> DeleteCompany(string id, [FromBody] bool deleteAssociations)
    {
        var deleteCompanyCommand = new DeleteCompanyCommand(id, deleteAssociations);
        await _sender.Send(deleteCompanyCommand);

        return NoContent();
    }

    [HttpGet("count")]
    public async Task<IActionResult> GetCompanyCount()
    {      
        return Ok(await _sender.Send(new GetCompanyCountQuery()));
    }
}
