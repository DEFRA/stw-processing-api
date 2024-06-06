namespace STW.ProcessingApi.Function.Rules.Common;

using Constants;
using Helpers;
using Interfaces;
using Models;

public class ChedTypeRule : IRule
{
    public bool ShouldInvoke(SpsCertificate spsCertificate) => true;

    public void Invoke(SpsCertificate spsCertificate, IList<ValidationError> validationErrors)
    {
        var chedType = SpsCertificateHelper.GetChedType(spsCertificate.SpsExchangedDocument.IncludedSpsNote);

        if (chedType is null)
        {
            validationErrors.Add(new ValidationError(RuleErrorMessage.ChedTypeMissing, RuleErrorId.ChedTypeMissing));
        }
        else if (!ChedType.Values.Contains(chedType))
        {
            validationErrors.Add(new ValidationError(RuleErrorMessage.ChedTypeInvalid, RuleErrorId.ChedTypeInvalid));
        }
    }
}
