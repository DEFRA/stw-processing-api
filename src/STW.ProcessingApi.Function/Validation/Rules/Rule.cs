namespace STW.ProcessingApi.Function.Validation.Rules;

using Models;

public abstract class Rule
{
    protected List<ValidationError> Errors { get; } = [];

    public abstract List<ValidationError> Validate(SpsCertificate spsCertificate);
}
