namespace STW.ProcessingApi.Function.Services;

using Models;

public interface IValidationService
{
    Task<List<ValidationError>> InvokeRulesAsync(SpsCertificate spsCertificate);
}
