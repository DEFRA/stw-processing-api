namespace STW.ProcessingApi.Function.Validation.Interfaces;

public interface IRuleValidator
{
    Task<bool> IsValidAsync(string input);
}
