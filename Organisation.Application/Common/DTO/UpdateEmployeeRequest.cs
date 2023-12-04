namespace Organisation.Application.Common.DTO;

public record UpdateEmployeeRequest(string Name, int Age, string Position, DateTime ModifiedOn, decimal Salary, string CompanyId);