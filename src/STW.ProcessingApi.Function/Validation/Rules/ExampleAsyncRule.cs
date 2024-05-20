using STW.ProcessingApi.Function.Validation.Interfaces;

namespace STW.ProcessingApi.Function.Validation.Rules;

public class ExampleAsyncRule : IAsyncRule
{
    public async Task<bool> ValidateAsync(string input)
    {
        return await Task.FromResult(!string.IsNullOrEmpty(input));
    }
}
