using STW.ProcessingApi.Function.Validation.Interfaces;

namespace STW.ProcessingApi.Function.Validation.Rules;

public class ExampleRule : IRule
{
    public bool Validate(string input)
    {
        return !string.IsNullOrEmpty(input);
    }
}
