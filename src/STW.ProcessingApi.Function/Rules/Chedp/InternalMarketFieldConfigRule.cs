namespace STW.ProcessingApi.Function.Rules.Chedp;

using Constants;
using Helpers;
using Interfaces;
using Models;
using Services.Interfaces;

public class InternalMarketFieldConfigRule : IAsyncRule
{
    private readonly IFieldConfigService _fieldConfigService;

    public InternalMarketFieldConfigRule(IFieldConfigService fieldConfigService)
    {
        _fieldConfigService = fieldConfigService;
    }

    public bool ShouldInvoke(SpsCertificate spsCertificate)
    {
        var chedType = SpsCertificateHelper.GetChedType(spsCertificate.SpsExchangedDocument.IncludedSpsNote);
        var purpose = PurposeHelper.GetPurpose(spsCertificate.SpsExchangedDocument.SignatorySpsAuthentication);

        return chedType == ChedType.Chedp && purpose == Purpose.InternalMarket;
    }

    public async Task InvokeAsync(SpsCertificate spsCertificate, IList<ValidationError> errors)
    {
        var result = await FetchFieldConfig(spsCertificate);
        if (!result.IsSuccess)
        {
                errors.Add(
                    new ValidationError(
                        string.Format(RuleErrorMessage.FieldConfigServiceError, result.Error.Message),
                        RuleErrorId.FieldConfigServiceError));
                return;
        }

        var speciesTypeName = ComplementHelper.GetSpeciesTypeName(SpsCertificateHelper.GetApplicableSpsClassifications(spsCertificate));
        var goodsCertifiedAs = PurposeHelper.GetGoodsCertifiedAs(spsCertificate.SpsExchangedDocument.SignatorySpsAuthentication)!;
        var validPurposes = FieldConfigHelper.GetValidPurposes(result.Value!.Data, speciesTypeName);
        if (!validPurposes.Contains(goodsCertifiedAs))
        {
            errors.Add(
                new ValidationError(
                    string.Format(RuleErrorMessage.InvalidInternalMarketPurposeError, goodsCertifiedAs),
                    RuleErrorId.InvalidInternalMarketPurposeError));
        }
    }

    private async Task<Result<FieldConfigDto>> FetchFieldConfig(SpsCertificate spsCertificate)
    {
        var chedType = SpsCertificateHelper.GetChedType(spsCertificate.SpsExchangedDocument.IncludedSpsNote)!;
        var applicableSpsClassification = SpsCertificateHelper.GetApplicableSpsClassifications(spsCertificate);
        var commodityCode = CommodityHelper.GetCommodityId(applicableSpsClassification)!;
        return await _fieldConfigService.GetByCertTypeAndCommodityCode(chedType, commodityCode);
    }
}
