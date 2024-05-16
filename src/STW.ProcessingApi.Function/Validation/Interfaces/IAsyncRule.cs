namespace STW.ProcessingApi.Function.Validation.Interfaces;

public interface IAsyncRule
{
    Task<bool> ValidateAsync(string input);
}
