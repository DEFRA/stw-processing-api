namespace STW.ProcessingApi.Function.Models;

public class Result<T>
    where T : class
{
    public bool IsSuccess { get; set; }

    public T? Value { get; set; }

    private Result(bool isSuccess, T? value)
    {
        IsSuccess = isSuccess;
        Value = value;
    }

    public static Result<T> Success(T value) => new Result<T>(true, value);

    public static Result<T> Failure => new Result<T>(false, default);
}
