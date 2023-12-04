
using ErrorOr;
using Organisation.Domain.Common.Errors.Custom;

namespace Organisation.Domain.Common.Errors;

public static partial class Errors
{
    public static class User
    {
        public static Error IncorrectEmailOrPassword(string msg = null) => Error.Validation(code: "VALERRUSR003", description: msg ?? "Email or Password is Incorrect");
        public static Error InvalidRefreshToken(string msg = null) => Error.Custom(Convert.ToInt32(CustomErrorType.UnAuthorized), "AUTHERRUSR001", msg ?? "Refresh token is invalid");
        public static Error RefreshTokenExpired(string msg = null) => Error.Custom(Convert.ToInt32(CustomErrorType.UnAuthorized), "AUTHERRUSR002", msg ?? "Refresh token has expired");
    }
}
