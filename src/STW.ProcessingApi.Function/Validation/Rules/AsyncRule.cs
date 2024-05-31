using STW.ProcessingApi.Function.Models;

namespace STW.ProcessingApi.Function.Validation.Rules;

public abstract class AsyncRule
{
    protected List<ValidationError> Errors { get; } = [];

    public abstract Task<List<ValidationError>> Validate(SpsCertificate spsCertificate);
}
