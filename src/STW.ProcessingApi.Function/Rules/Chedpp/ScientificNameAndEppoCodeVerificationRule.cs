namespace STW.ProcessingApi.Function.Rules.Chedpp;

using Constants;
using Helpers;
using Interfaces;
using Models;
using Services.Interfaces;

public class ScientificNameAndEppoCodeVerificationRule : IAsyncRule
{
    private readonly ICommodityCodeService _commodityCodeService;

    public ScientificNameAndEppoCodeVerificationRule(ICommodityCodeService commodityCodeService)
    {
        _commodityCodeService = commodityCodeService;
    }

    public bool ShouldInvoke(SpsCertificate spsCertificate)
    {
        var chedType = SpsCertificateHelper.GetChedType(spsCertificate.SpsExchangedDocument.IncludedSpsNote);

        return chedType == ChedType.Chedpp;
    }

    public async Task InvokeAsync(SpsCertificate spsCertificate, IList<ValidationError> validationErrors)
    {
        var tradeLinesItems = spsCertificate.SpsConsignment
            .IncludedSpsConsignmentItem
            .First()
            .IncludedSpsTradeLineItem;

        foreach (var tradeLineItem in tradeLinesItems)
        {
            var commodityId = CommodityHelper.GetCommodityId(tradeLineItem.ApplicableSpsClassification);
            var eppoCode = CommodityHelper.GetEppoCode(tradeLineItem.ApplicableSpsClassification);
            var speciesName = tradeLineItem.ScientificName[0].Value;

            var result = eppoCode is null
                ? await _commodityCodeService.GetCommodityInfoBySpeciesName(commodityId!, speciesName)
                : await _commodityCodeService.GetCommodityInfoByEppoCode(commodityId!, eppoCode);

            if (!result.IsSuccess)
            {
                validationErrors.Add(new ValidationError(RuleErrorMessage.CommodityCodeClientError, RuleErrorId.CommodityCodeClientError));

                return;
            }

            if (result.Value!.TotalElements == 0)
            {
                var errorMessage = eppoCode is null
                    ? string.Format(RuleErrorMessage.InvalidScientificName, speciesName, commodityId)
                    : string.Format(RuleErrorMessage.InvalidEppoCode, eppoCode, commodityId);

                var errorId = eppoCode is null
                    ? RuleErrorId.InvalidScientificName
                    : RuleErrorId.InvalidEppoCode;

                validationErrors.Add(new ValidationError(errorMessage, errorId, tradeLineItem.SequenceNumeric.Value));
            }
        }
    }
}
