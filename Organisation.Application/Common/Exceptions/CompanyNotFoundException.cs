

using Organisation.Application.Common.Interfaces.Exceptions;
using System.Net;

namespace Organisation.Application.Common.Exceptions;

public sealed class CompanyNotFoundException : Exception, IApplicationException
{
    public CompanyNotFoundException(string errorMessage) : base(errorMessage)
    {

    }
    public HttpStatusCode StatusCode => HttpStatusCode.NotFound;

    public string ErrorMessage => Message;
}
