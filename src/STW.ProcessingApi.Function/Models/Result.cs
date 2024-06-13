namespace STW.ProcessingApi.Function.Models;

using System.Net;

public class Result<T>
    where T : class
{
    private Result(bool isSuccess, T? value, HttpStatusCode? statusCode)
    {
        IsSuccess = isSuccess;
        Value = value;
        StatusCode = statusCode;
    }

    public bool IsSuccess { get; set; }

    public T? Value { get; set; }

    public Exception Error { get; set; }

    public HttpStatusCode? StatusCode { get; set; }

    public static Result<T> Success(T value) => new(true, value, default);

    public static Result<T> Failure(Exception error) => new(false, default, default)
    {
        Error = error
    };

    public static Result<T> Failure(HttpStatusCode statusCode) => new Result<T>(false, default, statusCode);
}
