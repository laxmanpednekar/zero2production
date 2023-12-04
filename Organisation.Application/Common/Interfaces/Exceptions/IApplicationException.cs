
using System.Net;
namespace Organisation.Application.Common.Interfaces.Exceptions;

public interface IApplicationException
{
    public HttpStatusCode StatusCode { get; }
    public string ErrorMessage { get; }
}