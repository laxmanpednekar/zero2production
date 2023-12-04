using Organisation.Application.Common.Interfaces.Exceptions;
using System.Net;

namespace Organisation.Application.Common.Exceptions;

public sealed class DuplicateCompanyException : Exception,IApplicationException
{
    public DuplicateCompanyException(string errorMessage) : base(errorMessage)
    {

    }

    public HttpStatusCode StatusCode => HttpStatusCode.Conflict;

    public string ErrorMessage => Message;
}
