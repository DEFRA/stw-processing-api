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

    public async Task<bool> IsValidAsync(string input)
    {
        var asyncRuleResult = await RunAsyncRules(input);
        return RunRules(input) && asyncRuleResult;
    }

    private bool RunRules(string input)
    {
        foreach (var rule in _rules)
        {
            if (!rule.Validate(input))
            {
                return false;
            }
        }

        return true;
    }

    private async Task<bool> RunAsyncRules(string input)
    {
        foreach (var asyncRule in _asyncRules)
        {
            if (!await asyncRule.ValidateAsync(input))
            {
                return false;
            }
        }

        return true;
    }
}
