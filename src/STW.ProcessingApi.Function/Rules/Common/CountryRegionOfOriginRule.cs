namespace STW.ProcessingApi.Function.Rules.Common;

using Constants;
using Helpers;
using Interfaces;
using Models;

public class CountryRegionOfOriginRule : IRule
{
    public bool ShouldInvoke(SpsCertificate spsCertificate)
    {
        var chedType = SpsCertificateHelper.GetChedType(spsCertificate.SpsExchangedDocument.IncludedSpsNote);

        return chedType is not null && ChedType.Values.Contains(chedType);
    }

    public void Invoke(SpsCertificate spsCertificate, IList<ValidationError> validationErrors)
    {
        var tradeLineItems = spsCertificate.SpsConsignment
            .IncludedSpsConsignmentItem
            .First()
            .IncludedSpsTradeLineItem
            .ToList();

        ValidateTradeLineItems(tradeLineItems, validationErrors);
    }

    private static void ValidateTradeLineItems(IList<IncludedSpsTradeLineItem> tradeLineItems, IList<ValidationError> validationErrors)
    {
        HashSet<string> countries = new HashSet<string>();
        HashSet<string> regions = new HashSet<string>();

        tradeLineItems
            .Where(x => IsCountryOfOriginPresent(x, validationErrors))
            .ToList()
            .ForEach(x => ValidateCountryAndRegion(x.OriginSpsCountry, countries, regions, validationErrors));
    }

    private static void ValidateCountryAndRegion(IList<SpsCountryType> spsCountryTypes, HashSet<string> countries, HashSet<string> regions, IList<ValidationError> validationErrors)
    {
        spsCountryTypes
            .Select(x => CheckCountryCodes(x, countries, validationErrors))
            .Where(x => HasValidRegionEntry(x, validationErrors))
            .Where(x => x.SubordinateSpsCountrySubDivision.First().Name.Count > 0)
            .ToList()
            .ForEach(x => ValidateRegion(x, regions, validationErrors));
    }

    private static SpsCountryType CheckCountryCodes(SpsCountryType spsCountryType, HashSet<string> countries, IList<ValidationError> validationErrors)
    {
        countries.Add(spsCountryType.Id.Value);

        if (countries.Count > 1)
        {
            validationErrors.Add(
                new ValidationError(
                string.Format(RuleErrorMessage.MoreThanOneCountryOfOrigin, string.Join(", ", countries)),
                RuleErrorId.MoreThanOneCountryOfOrigin));
        }

        return spsCountryType;
    }

    private static void ValidateRegion(SpsCountryType spsCountryType, HashSet<string> regions, IList<ValidationError> validationErrors)
    {
        var regionName = spsCountryType.SubordinateSpsCountrySubDivision[0].Name[0].Value;

        regions.Add(regionName);

        if (regions.Count > 1)
        {
            validationErrors.Add(
                new ValidationError(
                string.Format(RuleErrorMessage.MoreThanOneRegionOfOriginInConsignment, string.Join(", ", regions)),
                RuleErrorId.MoreThanOneRegionOfOriginInConsignment));
        }

        if (!regionName.StartsWith($"{spsCountryType.Id.Value}-"))
        {
            validationErrors.Add(
                new ValidationError(
                string.Format(RuleErrorMessage.InvalidRegionOfOrigin, regionName),
                RuleErrorId.InvalidRegionOfOrigin));
        }
    }

    private static bool HasValidRegionEntry(SpsCountryType spsCountryType, IList<ValidationError> validationErrors)
    {
        var subDivisionCount = spsCountryType.SubordinateSpsCountrySubDivision.Count;

        if (subDivisionCount == 1)
        {
            return true;
        }

        if (subDivisionCount > 1)
        {
            var set = spsCountryType.SubordinateSpsCountrySubDivision
                .SelectMany(x => x.Name)
                .Select(x => x.Value)
                .ToHashSet();

            validationErrors.Add(
                new ValidationError(
                    string.Format(RuleErrorMessage.MoreThanOneRegionOfOriginInTradeLineItem, string.Join(", ", set)),
                    RuleErrorId.MoreThanOneRegionOfOriginInTradeLineItem));
        }

        return false;
    }

    private static bool IsCountryOfOriginPresent(IncludedSpsTradeLineItem tradeLineItem, IList<ValidationError> validationErrors)
    {
        if (tradeLineItem.OriginSpsCountry.Count > 0)
        {
            return true;
        }

        validationErrors.Add(new ValidationError(RuleErrorMessage.CountryOfOriginMissing, RuleErrorId.CountryOfOriginMissing));

        return false;
    }
}
