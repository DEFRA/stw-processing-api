namespace STW.ProcessingApi.Function.Rules.Chedp;

using Constants;
using Helpers;
using Interfaces;
using Models;

public class PurposeOfConsignmentRule : IRule
{
    public bool ShouldInvoke(SpsCertificate spsCertificate)
    {
        var chedType = SpsCertificateHelper.GetChedType(spsCertificate.SpsExchangedDocument.IncludedSpsNote);

        return chedType == ChedType.Chedp;
    }

    public void Invoke(SpsCertificate spsCertificate, IList<ErrorEvent> errorEvents)
    {
        var conformsToEu = PurposeHelper.ConsignmentConformsToEu(spsCertificate.SpsExchangedDocument.IncludedSpsNote);
        var destinationType = PurposeHelper.GetNonConformingGoodsDestinationType(spsCertificate.SpsExchangedDocument.IncludedSpsNote);

        if (!conformsToEu && destinationType is null)
        {
            errorEvents.Add(new ErrorEvent(RuleErrorMessage.NonConformingConsignmentMissingDestinationType));
        }
    }
}
