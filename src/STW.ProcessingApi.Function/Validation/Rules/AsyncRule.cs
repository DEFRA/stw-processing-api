namespace STW.ProcessingApi.Function.Validation.Rules;

using Models;

public abstract class AsyncRule
{
    protected List<ValidationError> Errors { get; } = [];

    public abstract Task<List<ValidationError>> Validate(SpsCertificate spsCertificate);
}
