using ErrorOr;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Organisation.Application.Common.PipelineBehaviours;

public sealed class LoggingPipelineBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
     where TRequest : IRequest<TResponse>
    where TResponse : IErrorOr
{
    private readonly ILogger<LoggingPipelineBehaviour<TRequest, TResponse>> _logger;
    public LoggingPipelineBehaviour(ILogger<LoggingPipelineBehaviour<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting {RequestName} request at {DateTimeUTC}", typeof(TRequest).Name, DateTime.UtcNow);

        var result = await next();

        if (result.IsError)
        {
            _logger.LogError("Request {RequestName} failed at {DateTimeUTC}, {Error}", typeof(TRequest).Name, DateTime.UtcNow, result.Errors?.Select(e => e));
        }

        _logger.LogInformation("Completed {RequestName} request at {DateTimeUTC}", typeof(TRequest).Name, DateTime.UtcNow);

        return result;
    }
}
