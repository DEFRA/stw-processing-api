using STW.ProcessingApi.Function.Models;

namespace STW.ProcessingApi.Function.Validation.Rules;

public interface IRule
{
    List<ValidationError> Validate(SpsCertificate spsCertificate);
}
