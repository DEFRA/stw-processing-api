namespace STW.ProcessingApi.Function.Rules.Common;

using Constants;
using Helpers;
using Interfaces;
using Models;

public class SystemIdRule : IRule
{
    private const string CnSystemId = "CN";
    private readonly List<string> _chedTypes = [ChedType.Chedpp, ChedType.Chedp];

    public bool ShouldInvoke(SpsCertificate spsCertificate)
    {
        var chedType = SpsCertificateHelper.GetChedType(spsCertificate.SpsExchangedDocument.IncludedSpsNote);

        return chedType is not null && _chedTypes.Contains(chedType);
    }

    public void Invoke(SpsCertificate spsCertificate, IList<ErrorEvent> errorEvents)
    {
        var match = spsCertificate.SpsConsignment.IncludedSpsConsignmentItem
            .First()
            .IncludedSpsTradeLineItem
            .FirstOrDefault(x => !x.ApplicableSpsClassification.All(HasValidSystemId));

        if (match is not null)
        {
            errorEvents.Add(new ErrorEvent(RuleErrorMessage.SystemIdMissing, RuleErrorId.SystemIdMissing, match.SequenceNumeric.Value));
        }
    }

    private static bool HasValidSystemId(ApplicableSpsClassification spsClassification)
    {
        return spsClassification.SystemId?.Value == CnSystemId;
    }
}
