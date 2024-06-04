namespace STW.ProcessingApi.Function.Rules.Common;

using Constants;
using Helpers;
using Interfaces;
using Models;

public class SystemIdRule : IRule
{
    private const string CnSystemId = "CN";
    private readonly string[] _chedTypes = new string[] { ChedType.Chedp, ChedType.Chedpp };

    public bool ShouldInvoke(SpsCertificate spsCertificate)
    {
        var chedType = SpsCertificateHelper.GetChedType(spsCertificate.SpsExchangedDocument.IncludedSpsNote);

        return chedType is not null && _chedTypes.Contains(chedType);
    }

    public void Invoke(SpsCertificate spsCertificate, IList<ValidationError> validationErrors)
    {
        var match = spsCertificate.SpsConsignment.IncludedSpsConsignmentItem
            .First()
            .IncludedSpsTradeLineItem
            .FirstOrDefault(x => !x.ApplicableSpsClassification.All(HasValidSystemId));

        if (match is not null)
        {
            validationErrors.Add(new ValidationError(RuleErrorMessage.SystemIdMissing, RuleErrorId.SystemIdMissing, match.SequenceNumeric.Value));
        }
    }

    private static bool HasValidSystemId(ApplicableSpsClassification spsClassification)
    {
        return spsClassification.SystemId?.Value == CnSystemId;
    }
}
