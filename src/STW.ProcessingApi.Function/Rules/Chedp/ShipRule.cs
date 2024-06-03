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

    public void Invoke(SpsCertificate spsCertificate, IList<ValidationError> validationErrors)
    {
        var spsNoteTypes = spsCertificate.SpsExchangedDocument.IncludedSpsNote;

        if (PurposeHelper.ConsignmentConformsToEu(spsNoteTypes))
        {
            validationErrors.Add(new ValidationError(RuleErrorMessage.NonConformingGoodsCannotConformToEu, RuleErrorId.NonConformingGoodsCannotConformToEu));
        }

        if (PurposeHelper.GetNonConformingGoodsDestinationShipName(spsNoteTypes) is null)
        {
            validationErrors.Add(new ValidationError(RuleErrorMessage.NonConformingGoodsShipNameMissing, RuleErrorId.NonConformingGoodsShipNameMissing));
        }

        if (PurposeHelper.GetNonConformingGoodsDestinationShipPort(spsNoteTypes) is null)
        {
            validationErrors.Add(new ValidationError(RuleErrorMessage.NonConformingGoodsShipPortMissing, RuleErrorId.NonConformingGoodsShipPortMissing));
        }
    }
}
