using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Organisation.Application.Common.DTO;
using Organisation.Application.Common.Interfaces.Persistance;
using Organisation.Application.EmployeeModule.Commands.AddEmployee;
using Organisation.Application.EmployeeModule.Commands.DeleteEmployee;
using Organisation.Application.EmployeeModule.Commands.UpdateEmployee;
using Organisation.Application.EmployeeModule.Queries.GetEmployeeById;
using Organisation.Application.EmployeeModule.Queries.GetEmployees;
using Organisation.Domain.Employee;
using Organisation.Domain.Models;

namespace Organisation.Presentation.API.Controllers.V1;

[ApiController]
[Route("api/v{v:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public sealed class EmployeesController : BaseAPIController
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;
    public EmployeesController(ISender sender, IMapper mapper)
    {
        _sender = sender;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetEmployees([FromQuery] EmployeeQueryParameters queryParameters)
    {
        return Ok(await _sender.Send(new GetEmployeesQuery(queryParameters)));
    }

    [HttpGet("employee/{id:length(22)}")]
    public async Task<IActionResult> GetEmployeeById(string id)
    {
        return Ok(await _sender.Send(new GetEmployeeByIdQuery(id)));
    }

    [HttpPost("employee")]
    public async Task<IActionResult> AddEmployee(CreateEmployeeRequest createEmployeeRequest)
    {
        var addEmployeeCommand = _mapper.Map<AddEmployeeCommand>(createEmployeeRequest); //new AddEmployeeCommand(createEmployeeRequest);
        var result = await _sender.Send(addEmployeeCommand);
        return result.Match(
                  empId => CreatedAtAction("GetEmployeeById", new { id = empId }, createEmployeeRequest),
                  errors => Problem(errors)
               );
    }

    [HttpPut("employee/{id:length(22)}")]
    public async Task<IActionResult> UpdateEmployee(string id, [FromBody] UpdateEmployeeRequest updateEmployeeRequest)
    {
        await _sender.Send(new UpdateEmployeeCommand(
                                   id,
                                   updateEmployeeRequest.Name,
                                   updateEmployeeRequest.Age,
                                   updateEmployeeRequest.Position,
                                   updateEmployeeRequest.ModifiedOn,
                                   updateEmployeeRequest.Salary,
                                   updateEmployeeRequest.CompanyId));
        return NoContent();
    }

    [HttpDelete("employee/{id:length(22)}")]
    public async Task<IActionResult> DeleteEmployee(string id, [FromBody] bool deleteAssociations)
    {
        await _sender.Send(new DeleteEmployeeCommand(id, deleteAssociations));
        return NoContent();
    }
}