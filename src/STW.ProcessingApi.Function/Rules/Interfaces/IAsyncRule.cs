namespace STW.ProcessingApi.Function.Rules.Interfaces;

using Models;

public interface IAsyncRule
{
    bool ShouldInvoke(SpsCertificate spsCertificate);

    Task Invoke(SpsCertificate spsCertificate, IList<ValidationError> errors);
}
