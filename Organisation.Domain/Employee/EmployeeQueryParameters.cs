
using Organisation.Domain.Common.Utilities;

namespace Organisation.Domain.Employee;

public sealed class EmployeeQueryParameters : QueryParameters
{
    public string Name { get; set; } = string.Empty;
}
