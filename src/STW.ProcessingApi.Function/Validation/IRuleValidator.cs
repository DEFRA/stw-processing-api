namespace STW.ProcessingApi.Function.Validation;

using Models;

public interface IRuleValidator
{
    Task<List<ValidationError>> IsValid(SpsCertificate spsCertificate);
}
