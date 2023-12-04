using ErrorOr;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Organisation.Application.Common.Utilities;
using Organisation.Domain.Common.Errors.Custom;

namespace Organisation.Presentation.API.Controllers;

[Authorize]
public class BaseAPIController : ControllerBase
{
    protected IActionResult Problem(List<Error> errors) {
        if (errors.Count is 0)
            return Problem();

        if(errors.All(error=>error.Type == ErrorType.Validation))
            return ValidationProblem(errors);

        HttpContext.Items[GlobalConstants.Errors] = errors;

        return Problem(errors[0]);
    }

    private IActionResult Problem(Error error)
    {
        var statusCode = (int)error.Type switch
        {
            (int)ErrorType.Conflict => StatusCodes.Status409Conflict,
            (int)ErrorType.Validation => StatusCodes.Status400BadRequest,
            (int)ErrorType.NotFound => StatusCodes.Status404NotFound,
            (int)CustomErrorType.UnAuthorized => StatusCodes.Status401Unauthorized,
            (int)CustomErrorType.Forbidden => StatusCodes.Status403Forbidden,
            _ => StatusCodes.Status500InternalServerError
        };

        return Problem(statusCode: statusCode, title: error.Description);
    }

    private IActionResult ValidationProblem(List<Error> errors)
    {
        var modelStateDictionary = new ModelStateDictionary();

        foreach (var error in errors)
            modelStateDictionary.AddModelError(error.Code, error.Description);

        return ValidationProblem(modelStateDictionary);
    }
}
