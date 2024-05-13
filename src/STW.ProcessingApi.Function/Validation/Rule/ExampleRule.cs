namespace STW.ProcessingApi.Function.Validation.Rule;

public class ExampleRule : IRule
{
    public bool Validate(string input)
    {
        return !string.IsNullOrEmpty(input);
    }
}
