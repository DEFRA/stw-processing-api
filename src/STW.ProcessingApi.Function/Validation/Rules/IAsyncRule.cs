using STW.ProcessingApi.Function.Models;

namespace STW.ProcessingApi.Function.Validation.Rules;

public interface IAsyncRule
{
    Task<List<ValidationError>> ValidateAsync(SpsCertificate spsCertificate);
}
