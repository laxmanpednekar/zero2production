using ErrorOr;

namespace Organisation.Domain.Common.Errors;

public static partial class Errors
{
    public static class Employee
    {
        public static Error DuplicateEmployee(string msg = null) => Error.Conflict(code: "ERREMP000", description: msg ?? "Employee already exists in the system.");
        public static Error EmployeeDoesNotExist(string msg = null) => Error.NotFound(code: "ERREMP001", description: msg ?? "Employee does not exits in the system.");
    }
}