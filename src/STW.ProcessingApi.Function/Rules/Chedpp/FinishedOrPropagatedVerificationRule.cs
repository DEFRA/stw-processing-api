namespace STW.ProcessingApi.Function.Rules.Chedpp;

using Constants;
using Helpers;
using Interfaces;
using Models;
using Services.Interfaces;

public class FinishedOrPropagatedVerificationRule : IAsyncRule
{
    private readonly ICommodityCodeService _commodityCodeService;

    public FinishedOrPropagatedVerificationRule(ICommodityCodeService commodityCodeService)
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
        var tradeLineItems = spsCertificate.SpsConsignment
            .IncludedSpsConsignmentItem
            .First()
            .IncludedSpsTradeLineItem;

        var uniqueCommodityCodes = tradeLineItems.SelectMany(x => x.ApplicableSpsClassification)
            .Where(x => x.SystemId!.Value == SystemId.Cn)
            .Select(x => x.ClassCode!.Value)
            .Distinct()
            .ToList();

        var result = await _commodityCodeService.GetCommodityConfigurations(uniqueCommodityCodes);

        if (!result.IsSuccess)
        {
            validationErrors.Add(new ValidationError(RuleErrorMessage.CommodityCodeClientError, RuleErrorId.CommodityCodeClientError));

            return;
        }

        foreach (var configuration in result.Value!)
        {
            var commodityCode = configuration.CommodityCode;

            foreach (var tradeLineItem in tradeLineItems.Where(x => HasCommodityCode(x.ApplicableSpsClassification, commodityCode)))
            {
                var requiresFinishedOrPropagated = configuration.RequiresFinishedOrPropagated;
                var hasValidFinishedOrPropagatedValue = HasValidFinishedOrPropagatedValue(tradeLineItem.AdditionalInformationSpsNote);

                if (requiresFinishedOrPropagated && !hasValidFinishedOrPropagatedValue)
                {
                    var errorMessage = string.Format(RuleErrorMessage.MissingFinishedOrPropagated, tradeLineItem.SequenceNumeric.Value);
                    validationErrors.Add(new ValidationError(errorMessage, RuleErrorId.MissingFinishedOrPropagated));

                    return;
                }

                if (!requiresFinishedOrPropagated && hasValidFinishedOrPropagatedValue)
                {
                    var errorMessage = string.Format(RuleErrorMessage.FinishedOrPropagatedNotRequired, commodityCode);
                    validationErrors.Add(new ValidationError(errorMessage, RuleErrorId.FinishedOrPropagatedNotRequired));
                }
            }
        }
    }

    private static bool HasCommodityCode(IList<ApplicableSpsClassification> applicableSpsClassifications, string commodityCode)
    {
        return applicableSpsClassifications.Any(x => x.SystemId!.Value == SystemId.Cn && x.ClassCode!.Value == commodityCode);
    }

    private static bool HasValidFinishedOrPropagatedValue(IList<SpsNoteType> spsNoteTypes)
    {
        var finishedOrPropagatedValue = ComplementHelper.GetFinishedOrPropagated(spsNoteTypes);

        return finishedOrPropagatedValue is not null && FinishedOrPropagated.Values.Contains(finishedOrPropagatedValue);
    }
}
