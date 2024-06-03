namespace STW.ProcessingApi.Function.Validation.Rules;

using Models;

public class ExampleAsyncRule : AsyncRule
{
    public override async Task<List<ValidationError>> Validate(SpsCertificate spsCertificate)
    {
        return await Task.FromResult(new List<ValidationError>());
    }
}
