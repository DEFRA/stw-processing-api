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

    public void Invoke(SpsCertificate spsCertificate, IList<ValidationError> validationErrors)
    {
        ValidateExitBcp(spsCertificate.SpsConsignment.TransitSpsCountry, validationErrors);
        ValidateThirdCountry(spsCertificate.SpsConsignment.ImportSpsCountry, validationErrors);
        ValidateTransitingCountries(spsCertificate.SpsConsignment.TransitSpsCountry, validationErrors);
    }

    private static void ValidateExitBcp(IList<SpsCountryType> spsCountryTypes, IList<ValidationError> validationErrors)
    {
        var exitBcp = spsCountryTypes
            .Where(x => x.Id.Value == UnitedKingdomIsoCode)
            .SelectMany(x => x.SubordinateSpsCountrySubDivision)
            .SelectMany(x => x.ActivityAuthorizedSpsParty)
            .FirstOrDefault(x => x.Id is not null);

        if (exitBcp?.Id?.Value is null)
        {
            validationErrors.Add(new ValidationError(RuleErrorMessage.MissingExitBcp, RuleErrorId.MissingExitBcp));
        }
    }

    private static void ValidateThirdCountry(SpsCountryType spsCountryType, IList<ValidationError> validationErrors)
    {
        if (string.IsNullOrEmpty(spsCountryType.Id.Value))
        {
            validationErrors.Add(new ValidationError(RuleErrorMessage.ThirdCountryMissing, RuleErrorId.ThirdCountryMissing));
        }
    }

    private static void ValidateTransitingCountries(IList<SpsCountryType> spsCountryTypes, IList<ValidationError> validationErrors)
    {
        var transitingCountries = spsCountryTypes
            .Select(x => x.Id.Value)
            .Where(x => x != UnitedKingdomIsoCode)
            .ToList();

        if (transitingCountries.Count > MaxCountriesAllowed)
        {
            validationErrors.Add(new ValidationError(string.Format(RuleErrorMessage.TransitingCountriesMax, MaxCountriesAllowed), RuleErrorId.TransitingCountriesMax));
        }

        if (transitingCountries.Distinct().Count() < transitingCountries.Count)
        {
            validationErrors.Add(new ValidationError(RuleErrorMessage.DuplicateTransitingCountries, RuleErrorId.DuplicateTransitingCountries));
        }
    }
}
