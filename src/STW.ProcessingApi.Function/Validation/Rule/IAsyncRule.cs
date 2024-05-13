namespace STW.ProcessingApi.Function.Validation.Rule;

public interface IAsyncRule
{
    Task<bool> Validate(string input);
}
