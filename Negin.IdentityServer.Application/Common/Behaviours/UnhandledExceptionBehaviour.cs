using MediatR;
using Microsoft.Extensions.Logging;

namespace IdentityServer.Application.Common.Behaviours;

public class UnhandledExceptionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    private readonly ILogger<TRequest> _logger;

    public UnhandledExceptionBehaviour(ILogger<TRequest> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        try
        {
            TResponse? response = await next();
            return response;
            //return Result<TResponse>.Success(response);
        }
        catch (Exception ex)
        {
            if (ex is Exceptions.ValidationException)
            {
                IDictionary<string, string[]> errors = ((Exceptions.ValidationException)ex).Errors;
                if (errors.Any())
                {
                    List<string> _errors = new();
                    _errors.Add("پارامترهای ورودی صحیح نمی باشند" + Environment.NewLine);
                    foreach (KeyValuePair<string, string[]> error in errors)
                    {
                        _errors.Add($"Property: {error.Key} Error Code: {error.Value}" + Environment.NewLine);
                    }
                    _logger.LogError(string.Join(",", _errors));
                    throw new Exception(string.Join(",", _errors));
                    //return Result<TResponse>.Failure(_errors);
                }
            }
            string requestName = typeof(TRequest).Name;

            _logger.LogError(ex, "Request: Unhandled Exception for Request {Name} {@Request}", requestName, request);
            throw;
            //return Result<TResponse>.Failure(new List<string>{ (ex.InnerException!=null ? ex.InnerException.Message : ex.Message) });
        }
    }
}
