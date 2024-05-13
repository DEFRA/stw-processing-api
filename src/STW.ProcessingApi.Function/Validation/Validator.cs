using System.Collections.Immutable;
using STW.ProcessingApi.Function.Validation.Rule;

namespace STW.ProcessingApi.Function.Validation;

public class Validator : IValidator
{
    public bool IsValid(List<IRule> rules, List<IAsyncRule> asyncRules, string input)
    {
        return RunRules(rules, input) && RunAsyncRules(asyncRules, input).Result;
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
            if (!await asyncRule.Validate(input))
            {
                return false;
            }
        }

        return true;
    }
}
