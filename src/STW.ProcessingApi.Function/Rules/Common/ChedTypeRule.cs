namespace STW.ProcessingApi.Function.Rules.Common;

using Constants;
using Helpers;
using Interfaces;
using Models;

public class ChedTypeRule : IRule
{
    private readonly string[] _chedTypes = new string[] { ChedType.Chedp, ChedType.Chedpp };

    public bool ShouldInvoke(SpsCertificate spsCertificate) => true;

    public void Invoke(SpsCertificate spsCertificate, IList<ValidationError> validationErrors)
    {
        var chedType = SpsCertificateHelper.GetChedType(spsCertificate.SpsExchangedDocument.IncludedSpsNote);

        if (chedType is null)
        {
            validationErrors.Add(new ValidationError(RuleErrorMessage.ChedTypeMissing, RuleErrorId.ChedTypeMissing));
        }
        else if (!_chedTypes.Contains(chedType))
        {
            validationErrors.Add(new ValidationError(RuleErrorMessage.ChedTypeInvalid, RuleErrorId.ChedTypeInvalid));
        }
    }
}
