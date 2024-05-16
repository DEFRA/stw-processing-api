using STW.ProcessingApi.Function.Validation.Interfaces;

namespace STW.ProcessingApi.Function.Validation;

public class Validator : IRuleValidator
{
    private readonly List<IRule> _rules;
    private readonly List<IAsyncRule> _asyncRules;

    public Validator(List<IRule> rules, List<IAsyncRule> asyncRules)
    {
        _rules = rules;
        _asyncRules = asyncRules;
    }

    public bool IsValid(string input)
    {
        return RunRules(_rules, input) && RunAsyncRules(_asyncRules, input).Result;
    }

    private bool RunRules(List<IRule> rules, string input)
    {
        foreach (var rule in rules)
        {
            if (!rule.Validate(input))
            {
                return false;
            }
        }

        return true;
    }

    private async Task<bool> RunAsyncRules(List<IAsyncRule> asyncRules, string input)
    {
        foreach (var asyncRule in asyncRules)
        {
            if (!await asyncRule.ValidateAsync(input))
            {
                return false;
            }
        }

        return true;
    }
}
