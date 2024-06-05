namespace STW.ProcessingApi.Function.Rules.Interfaces;

using Models;

public interface IAsyncRule
{
    bool ShouldInvoke(SpsCertificate spsCertificate);

    Task InvokeAsync(SpsCertificate spsCertificate, IList<ValidationError> validationErrors);
}
