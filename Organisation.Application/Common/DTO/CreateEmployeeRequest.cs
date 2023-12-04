namespace Organisation.Application.Common.DTO;

public record CreateEmployeeRequest(string Name, int Age, string Position, DateTime CreatedOn, decimal Salary, string CompanyId);