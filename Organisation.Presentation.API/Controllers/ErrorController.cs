using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Organisation.Application.Common.Interfaces.Exceptions;
using Organisation.Domain.Common.Errors;
namespace Organisation.Presentation.API.Controllers;

[Route("/error")]
public sealed class ErrorController : BaseAPIController
{
    public IActionResult Error()
    {
        List<ErrorOr.Error> errors = new List<ErrorOr.Error>();
        var exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;
        errors.Add(Errors.Unexpected.InternalServerError(exception.Message));
        /* var (statusCode, message) = exception switch
         {
             IApplicationException appException => (Convert.ToInt32(appException.StatusCode), appException.ErrorMessage),
             _ => (StatusCodes.Status500InternalServerError, "An Unexpected error occured")
         };*/

        return Problem(errors);//Problem(statusCode: statusCode, title: message);
    }
}
