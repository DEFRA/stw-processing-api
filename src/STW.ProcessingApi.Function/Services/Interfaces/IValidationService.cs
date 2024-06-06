namespace STW.ProcessingApi.Function.Services.Interfaces;

using Models;

public interface IValidationService
{
    Task InvokeRulesAsync(SpsCertificate spsCertificate);
}
