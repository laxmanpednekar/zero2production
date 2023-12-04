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

namespace Organisation.Presentation.API.Controllers.V2;

[ApiController]
[Route("api/v{v:apiVersion}/[controller]")]
[ApiVersion("2.0")]
public sealed class EmployeesController : BaseAPIController
{
    private readonly ISender _sender;
    public EmployeesController(ISender sender)
    {
        _sender = sender;
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
        var addEmployeeCommand = new AddEmployeeCommand(createEmployeeRequest);
        var employeeId = await _sender.Send(addEmployeeCommand);
        return CreatedAtAction("GetEmployeeById", new { id = employeeId }, createEmployeeRequest);
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