namespace STW.ProcessingApi.Function.Rules.Chedp;

using Constants;
using Helpers;
using Interfaces;
using Models;

public class ShipRule : IRule
{
    public bool ShouldInvoke(SpsCertificate spsCertificate)
    {
        var chedType = SpsCertificateHelper.GetChedType(spsCertificate.SpsExchangedDocument.IncludedSpsNote);
        var purpose = PurposeHelper.GetPurpose(spsCertificate.SpsExchangedDocument.SignatorySpsAuthentication);
        var destinationType = PurposeHelper.GetNonConformingGoodsDestinationType(spsCertificate.SpsExchangedDocument.IncludedSpsNote);

        return chedType == ChedType.Chedp && purpose == Purpose.NonConformingGoods && destinationType == DestinationType.Ship;
    }

    public void Invoke(SpsCertificate spsCertificate, IList<ErrorEvent> errorEvents)
    {
        var spsNoteTypes = spsCertificate.SpsExchangedDocument.IncludedSpsNote;

        if (PurposeHelper.ConsignmentConformsToEu(spsNoteTypes))
        {
            errorEvents.Add(new ErrorEvent(RuleErrorMessage.NonConformingGoodsCannotConformToEu));
        }

        if (PurposeHelper.GetNonConformingGoodsDestinationShipName(spsNoteTypes) is null)
        {
            errorEvents.Add(new ErrorEvent(RuleErrorMessage.NonConformingGoodsShipNameMissing));
        }

        if (PurposeHelper.GetNonConformingGoodsDestinationShipPort(spsNoteTypes) is null)
        {
            errorEvents.Add(new ErrorEvent(RuleErrorMessage.NonConformingGoodsShipPortMissing));
        }
    }
}
