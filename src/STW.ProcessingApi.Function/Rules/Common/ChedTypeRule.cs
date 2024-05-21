namespace STW.ProcessingApi.Function.Rules.Common;

using Constants;
using Helpers;
using Interfaces;
using Models;

public class ChedTypeRule : IRule
{
    private readonly List<string> _validChedTypes = [ChedType.Chedp, ChedType.Chedpp];

    public bool ShouldInvoke(SpsCertificate spsCertificate) => true;

    public void Invoke(SpsCertificate spsCertificate, IList<ErrorEvent> errorEvents)
    {
        var chedType = SpsCertificateHelper.GetChedType(spsCertificate.SpsExchangedDocument.IncludedSpsNote);

        if (chedType is null)
        {
            errorEvents.Add(new ErrorEvent(RuleErrorMessage.ChedTypeMissing));
        }
        else if (!_validChedTypes.Contains(chedType))
        {
            errorEvents.Add(new ErrorEvent(RuleErrorMessage.ChedTypeInvalid));
        }
    }
}
