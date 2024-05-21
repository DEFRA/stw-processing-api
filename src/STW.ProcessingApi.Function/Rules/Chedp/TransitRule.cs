namespace STW.ProcessingApi.Function.Rules.Chedp;

using Constants;
using Helpers;
using Interfaces;
using Models;

public class TransitRule : IRule
{
    private const string UnitedKingdomIsoCode = "GB";
    private const int MaxCountriesAllowed = 12;

    public bool ShouldInvoke(SpsCertificate spsCertificate)
    {
        var chedType = SpsCertificateHelper.GetChedType(spsCertificate.SpsExchangedDocument.IncludedSpsNote);
        var purpose = PurposeHelper.GetPurpose(spsCertificate.SpsExchangedDocument.SignatorySpsAuthentication);

        return chedType == ChedType.Chedp && purpose == Purpose.DirectTransit;
    }

    public void Invoke(SpsCertificate spsCertificate, IList<ErrorEvent> errorEvents)
    {
        ValidateExitBcp(spsCertificate.SpsConsignment.TransitSpsCountry, errorEvents);
        ValidateThirdCountry(spsCertificate.SpsConsignment.ImportSpsCountry, errorEvents);
        ValidateTransitingCountries(spsCertificate.SpsConsignment.TransitSpsCountry, errorEvents);
    }

    private static void ValidateExitBcp(IList<SpsCountryType> spsCountryTypes, IList<ErrorEvent> errorEvents)
    {
        var exitBcp = spsCountryTypes
            .Where(x => x.Id.Value == UnitedKingdomIsoCode)
            .SelectMany(x => x.SubordinateSpsCountrySubDivision)
            .SelectMany(x => x.ActivityAuthorizedSpsParty)
            .FirstOrDefault(x => x.Id is not null);

        if (exitBcp?.Id?.Value is null)
        {
            errorEvents.Add(new ErrorEvent(RuleErrorMessage.MissingExitBcp));
        }
    }

    private static void ValidateThirdCountry(SpsCountryType spsCountryType, IList<ErrorEvent> errorEvents)
    {
        if (string.IsNullOrEmpty(spsCountryType.Id.Value))
        {
            errorEvents.Add(new ErrorEvent(RuleErrorMessage.ThirdCountryMissing));
        }
    }

    private static void ValidateTransitingCountries(IList<SpsCountryType> spsCountryTypes, IList<ErrorEvent> errorEvents)
    {
        var transitingCountries = spsCountryTypes
            .Select(x => x.Id.Value)
            .Where(x => x != UnitedKingdomIsoCode)
            .ToList();

        if (transitingCountries.Count > MaxCountriesAllowed)
        {
            errorEvents.Add(new ErrorEvent(string.Format(RuleErrorMessage.TransitingCountriesMax, MaxCountriesAllowed)));
        }

        if (transitingCountries.Distinct().Count() < transitingCountries.Count)
        {
            errorEvents.Add(new ErrorEvent(RuleErrorMessage.DuplicateTransitingCountries));
        }
    }
}
