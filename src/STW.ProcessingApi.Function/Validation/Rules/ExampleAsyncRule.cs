using STW.ProcessingApi.Function.Models;

namespace STW.ProcessingApi.Function.Validation.Rules;

public class ExampleAsyncRule : IAsyncRule
{
    public async Task<List<ValidationError>> ValidateAsync(SpsCertificate spsCertificate)
    {
        return await Task.FromResult(new List<ValidationError>());
    }
}
