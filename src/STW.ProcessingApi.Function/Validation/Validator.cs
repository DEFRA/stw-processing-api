using STW.ProcessingApi.Function.Models;
using STW.ProcessingApi.Function.Validation.Rules;

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

    public async Task<List<ValidationError>> IsValid(SpsCertificate spsCertificate)
    {
        var errors = new List<ValidationError>();

        errors.AddRange(_rules.SelectMany(rule => rule.Validate(spsCertificate)));
        errors.AddRange(
            (await Task.WhenAll(_asyncRules.Select(rule => rule.ValidateAsync(spsCertificate)))).SelectMany(x => x));

        return errors;
    }
}
