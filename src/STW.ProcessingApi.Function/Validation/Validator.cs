using STW.ProcessingApi.Function.Models;
using STW.ProcessingApi.Function.Validation.Rules;

namespace STW.ProcessingApi.Function.Validation;

public class Validator : IRuleValidator
{
    private readonly IEnumerable<Rule> _rules;
    private readonly IEnumerable<AsyncRule> _asyncRules;

    public Validator(IEnumerable<Rule> rules, IEnumerable<AsyncRule> asyncRules)
    {
        _rules = rules;
        _asyncRules = asyncRules;
    }

    public async Task<List<ValidationError>> IsValid(SpsCertificate spsCertificate)
    {
        var errors = new List<ValidationError>();

        errors.AddRange(_rules.SelectMany(rule => rule.Validate(spsCertificate)));
        errors.AddRange(
            (await Task.WhenAll(_asyncRules.Select(rule => rule.Validate(spsCertificate)))).SelectMany(x => x));

        return errors;
    }
}
