using STW.ProcessingApi.Function.Models;

namespace STW.ProcessingApi.Function.Validation;

public interface IRuleValidator
{
    Task<List<ValidationError>> IsValid(SpsCertificate spsCertificate);
}
