using ErrorOr;

namespace Organisation.Domain.Common.Errors;

public static partial class Errors
{
    public static class Company {

        public static Error DuplicateCompany(string msg = null) => Error.Conflict(code: "ERRCOMP000", description: msg ?? "Company already exists in the system.");
        public static Error CompanyDoestNotExist(string msg = null) => Error.NotFound(code: "ERRCOMP001", description: msg ?? "Company does not exits in the system.");
    }
}
