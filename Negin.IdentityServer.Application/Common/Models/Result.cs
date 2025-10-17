using System.Net;

namespace IdentityServer.Application.Common.Models;

public class Result
{
    internal Result(bool succeeded, HttpStatusCode? httpStatusCode, IEnumerable<string> errors)
    {
        Succeeded = succeeded;
        Errors = errors.ToArray();
        HttpStatusCode = httpStatusCode;
    }
    public bool Succeeded { get; private set; }
    public HttpStatusCode? HttpStatusCode { get; private set; }

    public string[] Errors { get; init; }

    public static Result Success()
    {
        return new Result(true, System.Net.HttpStatusCode.OK, Array.Empty<string>());
    }
    
    public static Result Failure(IEnumerable<string> errors)
    {
        return new Result(false, System.Net.HttpStatusCode.BadRequest, errors);
    }
}
public class Result<T>
{
    internal Result(bool succeeded, HttpStatusCode? httpStatusCode, IEnumerable<string> errors)
    {
        Succeeded = succeeded;
        Errors = errors.ToArray();
        HttpStatusCode = httpStatusCode;
    }
    internal Result(bool succeeded, HttpStatusCode? httpStatusCode, IEnumerable<string> errors, T data)
    {
        Succeeded = succeeded;
        Errors = errors.ToArray();
        Data = data;
        HttpStatusCode = httpStatusCode;    
    }
    public T? Data { get; set; }
    public bool Succeeded { get; private set; }
    public HttpStatusCode? HttpStatusCode { get; private set; }

    public string[] Errors { get; init; }

    public static Result<T> Success()
    {
        return new Result<T>(true, System.Net.HttpStatusCode.OK, Array.Empty<string>());
    }
    public static Result<T> Success(T data)
    {
        return new Result<T>(true, System.Net.HttpStatusCode.OK, Array.Empty<string>(), data);
    }
    public static Result<T> Failure(IEnumerable<string> errors)
    {
        return new Result<T>(false, System.Net.HttpStatusCode.BadRequest, errors);
    }
    public static Result<T> Failure(IEnumerable<string> errors, T data)
    {
        return new Result<T>(false, System.Net.HttpStatusCode.BadRequest, errors, data);
    }
}
