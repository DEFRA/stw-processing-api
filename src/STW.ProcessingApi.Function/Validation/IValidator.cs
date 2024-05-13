using STW.ProcessingApi.Function.Validation.Rule;

namespace STW.ProcessingApi.Function.Validation;

public interface IValidator
{
    bool IsValid(List<IRule> rules, List<IAsyncRule> asyncRules, string input);
}
