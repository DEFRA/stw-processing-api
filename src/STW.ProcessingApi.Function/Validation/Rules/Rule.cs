using STW.ProcessingApi.Function.Models;

namespace STW.ProcessingApi.Function.Validation.Rules;

public abstract class Rule
{
    protected List<ValidationError> Errors { get; } = [];

    public abstract List<ValidationError> Validate(SpsCertificate spsCertificate);
}
