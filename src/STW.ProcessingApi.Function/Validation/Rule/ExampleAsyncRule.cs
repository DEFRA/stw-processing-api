namespace STW.ProcessingApi.Function.Validation.Rule;

public class ExampleAsyncRule : IAsyncRule
{
    public async Task<bool> Validate(string input)
    {
        await Task.Delay(0); // just to make this example async
        return !string.IsNullOrEmpty(input);
    }
}
