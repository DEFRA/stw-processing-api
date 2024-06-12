namespace STW.ProcessingApi.Function.Rules.Chedp;

using Constants;
using Helpers;
using Interfaces;
using Models;
using Services.Interfaces;

public class CommodityIdSpeciesNameVerificationRule : IAsyncRule
{
    private readonly ICommodityCodeService _commodityCodeService;

    public CommodityIdSpeciesNameVerificationRule(ICommodityCodeService commodityCodeService)
    {
        _commodityCodeService = commodityCodeService;
    }

    public bool ShouldInvoke(SpsCertificate spsCertificate)
    {
        var chedType = SpsCertificateHelper.GetChedType(spsCertificate.SpsExchangedDocument.IncludedSpsNote);

        return chedType == ChedType.Chedp;
    }

    public async Task InvokeAsync(SpsCertificate spsCertificate, IList<ValidationError> validationErrors)
    {
        var tradeLineItems = spsCertificate.SpsConsignment
            .IncludedSpsConsignmentItem
            .First()
            .IncludedSpsTradeLineItem;

        foreach (var tradeLineItem in tradeLineItems)
        {
            var commodityId = CommodityHelper.GetCommodityId(tradeLineItem.ApplicableSpsClassification)!;

            var result = await _commodityCodeService.GetCommodityCategories(commodityId, OldChedType.Cvedp);

            if (!result.IsSuccess)
            {
                validationErrors.Add(new ValidationError(RuleErrorMessage.CommodityCodeClientError, RuleErrorId.CommodityCodeClientError));

                return;
            }

            var typeInfos = CommodityCategoryParser.Parse(result.Value!.Data);

            if (typeInfos.Count == 0)
            {
                var errorMessage = string.Format(RuleErrorMessage.CommodityCategoryDataNotFound, commodityId);
                validationErrors.Add(new ValidationError(errorMessage, RuleErrorId.CommodityCategoryDataNotFound));

                return;
            }

            var speciesTypeName = ComplementHelper.GetSpeciesTypeName(tradeLineItem.ApplicableSpsClassification);
            var typeInfo = typeInfos.FirstOrDefault(x => x.Type.Text == speciesTypeName);

            if (typeInfo is null)
            {
                var errorMessage = string.Format(RuleErrorMessage.SpeciesTypeNameNotRecognised, speciesTypeName);
                validationErrors.Add(new ValidationError(errorMessage, RuleErrorId.SpeciesTypeNameNotRecognised));

                return;
            }

            var speciesClassName = ComplementHelper.GetSpeciesClassName(tradeLineItem.ApplicableSpsClassification);

            if (typeInfo.TaxonomicClasses.FirstOrDefault(x => x.Text == speciesClassName) is null)
            {
                var errorMessage = string.Format(RuleErrorMessage.SpeciesClassNameNotRecognised, speciesClassName);
                validationErrors.Add(new ValidationError(errorMessage, RuleErrorId.SpeciesClassNameNotRecognised));
            }

            var speciesFamilyName = ComplementHelper.GetSpeciesFamilyName(tradeLineItem.ApplicableSpsClassification);

            if (typeInfo.TaxonomicFamilies.FirstOrDefault(x => x.Text == speciesFamilyName) is null)
            {
                var errorMessage = string.Format(RuleErrorMessage.SpeciesFamilyNameNotRecognised, speciesFamilyName);
                validationErrors.Add(new ValidationError(errorMessage, RuleErrorId.SpeciesFamilyNameNotRecognised));
            }

            var scientificName = tradeLineItem.ScientificName.First().Value;

            if (typeInfo.TaxonomicSpecies.FirstOrDefault(x => x.Text == scientificName) is null)
            {
                var errorMessage = string.Format(RuleErrorMessage.SpeciesNameNotRecognised, scientificName);
                validationErrors.Add(new ValidationError(errorMessage, RuleErrorId.SpeciesNameNotRecognised));
            }
        }
    }
}
