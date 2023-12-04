

using ErrorOr;

namespace Organisation.Domain.Common.Errors;

public static partial class Errors
{
    public static class Unexpected
    {
        public static Error InternalServerError(string msg = null) => Error.Unexpected(code: "ERRUNEXPECTED", description: msg ?? "An Unexpected error occured");
    }
}