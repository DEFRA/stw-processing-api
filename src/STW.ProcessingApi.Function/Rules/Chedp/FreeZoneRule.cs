namespace STW.ProcessingApi.Function.Rules.Chedp;

using Constants;
using Helpers;
using Interfaces;
using Models;

public class FreeZoneRule : IRule
{
    public bool ShouldInvoke(SpsCertificate spsCertificate)
    {
        var chedType = SpsCertificateHelper.GetChedType(spsCertificate.SpsExchangedDocument.IncludedSpsNote);
        var purpose = PurposeHelper.GetPurpose(spsCertificate.SpsExchangedDocument.SignatorySpsAuthentication);
        var destinationType = PurposeHelper.GetNonConformingGoodsDestinationType(spsCertificate.SpsExchangedDocument.IncludedSpsNote);

        return chedType == ChedType.Chedp && purpose == Purpose.NonConformingGoods && destinationType == DestinationType.FreeZone;
    }

    public void Invoke(SpsCertificate spsCertificate, IList<ValidationError> validationErrors)
    {
        var spsNoteTypes = spsCertificate.SpsExchangedDocument.IncludedSpsNote;

        if (PurposeHelper.ConsignmentConformsToEu(spsNoteTypes))
        {
            validationErrors.Add(new ValidationError(RuleErrorMessage.NonConformingGoodsCannotConformToEu, RuleErrorId.NonConformingGoodsCannotConformToEu));
        }

        if (PurposeHelper.GetNonConformingGoodsDestinationRegisteredNumber(spsNoteTypes) is null)
        {
            validationErrors.Add(new ValidationError(RuleErrorMessage.RegisteredNumberMissingFreeZoneNumber, RuleErrorId.RegisteredNumberMissingFreeZoneNumber));
        }
    }
}
