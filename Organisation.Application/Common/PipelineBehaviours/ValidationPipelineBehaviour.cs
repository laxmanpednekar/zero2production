using ErrorOr;
using FluentValidation;
using MediatR;

namespace Organisation.Application.Common.PipelineBehaviours;

public sealed class ValidationPipelineBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : IErrorOr
{
    private readonly IValidator<TRequest>? _validator;

    public ValidationPipelineBehaviour(IValidator<TRequest>? validator = null)
    {
        _validator = validator;
    }
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if(_validator is null)
           return await next();

        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (validationResult.IsValid)
            return await next();

        var errors = validationResult.Errors
                                     .ConvertAll(
                                          validationFailure => Error.Validation(
                                                                 code: validationFailure.ErrorCode ?? validationFailure.PropertyName,
                                                                 description: validationFailure.ErrorMessage)
                                      );

        return (dynamic)errors;
    }
}
