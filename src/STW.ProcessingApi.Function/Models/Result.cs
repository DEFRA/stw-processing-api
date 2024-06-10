namespace STW.ProcessingApi.Function.Models;

public class Result<T>
    where T : class
{
    private Result(bool isSuccess, T? value)
    {
        IsSuccess = isSuccess;
        Value = value;
    }

    public bool IsSuccess { get; set; }

    public T? Value { get; set; }

    public Exception Error { get; set; }

    public static Result<T> Success(T value) => new(true, value);

    public static Result<T> Failure() => new(false, default);

    public static Result<T> Failure(Exception error) => new(false, default)
    {
        Error = error
    };
}
