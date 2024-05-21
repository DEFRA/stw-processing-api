namespace STW.ProcessingApi.Function.Rules.Common;

using Constants;
using Helpers;
using Interfaces;
using Models;

public class CountryRegionOfOriginRule : IRule
{
    private readonly List<string> _chedTypes = [ChedType.Chedpp, ChedType.Chedp];

    public bool ShouldInvoke(SpsCertificate spsCertificate)
    {
        var chedType = SpsCertificateHelper.GetChedType(spsCertificate.SpsExchangedDocument.IncludedSpsNote);

        return chedType is not null && _chedTypes.Contains(chedType);
    }

    public void Invoke(SpsCertificate spsCertificate, IList<ErrorEvent> errorEvents)
    {
        var tradeLineItems = spsCertificate.SpsConsignment
            .IncludedSpsConsignmentItem
            .First()
            .IncludedSpsTradeLineItem
            .ToList();

        ValidateTradeLineItems(tradeLineItems, errorEvents);
    }

    private static void ValidateTradeLineItems(IList<IncludedSpsTradeLineItem> tradeLineItems, IList<ErrorEvent> errorEvents)
    {
        HashSet<string> countries = [];
        HashSet<string> regions = [];

        tradeLineItems
            .Where(x => IsCountryOfOriginPresent(x, errorEvents))
            .ToList()
            .ForEach(x => ValidateCountryAndRegion(x.OriginSpsCountry, countries, regions, errorEvents));
    }

    private static void ValidateCountryAndRegion(IList<SpsCountryType> spsCountryTypes, HashSet<string> countries, HashSet<string> regions, IList<ErrorEvent> errorEvents)
    {
        spsCountryTypes
            .Select(x => CheckCountryCodes(x, countries, errorEvents))
            .Where(x => HasValidRegionEntry(x, errorEvents))
            .Where(x => x.SubordinateSpsCountrySubDivision.First().Name.Count > 0)
            .ToList()
            .ForEach(x => ValidateRegion(x, regions, errorEvents));
    }

    private static SpsCountryType CheckCountryCodes(SpsCountryType spsCountryType, HashSet<string> countries, IList<ErrorEvent> errorEvents)
    {
        countries.Add(spsCountryType.Id.Value);

        if (countries.Count > 1)
        {
            errorEvents.Add(new ErrorEvent(string.Format(RuleErrorMessage.MoreThanOneCountryOfOrigin, string.Join(", ", countries))));
        }

        return spsCountryType;
    }

    private static void ValidateRegion(SpsCountryType spsCountryType, HashSet<string> regions, IList<ErrorEvent> errorEvents)
    {
        var regionName = spsCountryType.SubordinateSpsCountrySubDivision[0].Name[0].Value;

        regions.Add(regionName);

        if (regions.Count > 1)
        {
            errorEvents.Add(new ErrorEvent(string.Format(RuleErrorMessage.MoreThanOneRegionOfOriginInConsignment, string.Join(", ", regions))));
        }

        if (!regionName.StartsWith($"{spsCountryType.Id.Value}-"))
        {
            errorEvents.Add(new ErrorEvent(string.Format(RuleErrorMessage.InvalidRegionOfOrigin, regionName)));
        }
    }

    private static bool HasValidRegionEntry(SpsCountryType spsCountryType, IList<ErrorEvent> errorEvents)
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

            errorEvents.Add(new ErrorEvent(string.Format(RuleErrorMessage.MoreThanOneRegionOfOriginInTradeLineItem, string.Join(", ", set))));
        }

        return false;
    }

    private static bool IsCountryOfOriginPresent(IncludedSpsTradeLineItem tradeLineItem, IList<ErrorEvent> errorEvents)
    {
        if (tradeLineItem.OriginSpsCountry.Count > 0)
        {
            return true;
        }

        errorEvents.Add(new ErrorEvent(RuleErrorMessage.CountryOfOriginMissing));

        return false;
    }
}
