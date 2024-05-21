namespace STW.ProcessingApi.Function.Rules.Interfaces;

using Models;

public interface IRule
{
    bool ShouldInvoke(SpsCertificate spsCertificate);

    void Invoke(SpsCertificate spsCertificate, IList<ErrorEvent> errorEvents);
}
