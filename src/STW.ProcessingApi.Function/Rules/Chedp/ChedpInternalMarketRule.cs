namespace STW.ProcessingApi.Function.Rules.Chedp;

using Constants;
using Helpers;
using Interfaces;
using Models;

public class ChedpInternalMarketRule : IRule
{
    public bool ShouldInvoke(SpsCertificate spsCertificate)
    {
        var chedType = SpsCertificateHelper.GetChedType(spsCertificate.SpsExchangedDocument.IncludedSpsNote);
        var purpose = PurposeHelper.GetPurpose(spsCertificate.SpsExchangedDocument.SignatorySpsAuthentication);
        var goodsCertifiedAs = PurposeHelper.GetGoodsCertifiedAs(spsCertificate.SpsExchangedDocument.SignatorySpsAuthentication);

        return chedType == ChedType.Chedp && purpose == Purpose.InternalMarket && goodsCertifiedAs is not null;
    }

    public void Invoke(SpsCertificate spsCertificate, IList<ValidationError> validationErrors)
    {
        var goodsCertifiedAs = PurposeHelper.GetGoodsCertifiedAs(spsCertificate.SpsExchangedDocument.SignatorySpsAuthentication);

        if (GoodsCertifiedAs.Values().All(x => goodsCertifiedAs != x))
        {
            validationErrors.Add(new ValidationError(RuleErrorMessage.GoodsCertifiedAsValueIsInvalid, RuleErrorId.GoodsCertifiedAsValueIsInvalid));
        }

        if (!PurposeHelper.ConsignmentConformsToEu(spsCertificate.SpsExchangedDocument.IncludedSpsNote))
        {
            validationErrors.Add(new ValidationError(RuleErrorMessage.ConformsToEuRequiredForInternalMarket, RuleErrorId.ConformsToEuRequiredForInternalMarket));
        }
    }
}
