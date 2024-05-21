namespace STW.ProcessingApi.Function.Rules.Chedp;

using System.Net;
using Constants;
using Helpers;
using Interfaces;
using Models;
using Services.Interfaces;

public class TransitVerificationRule : IAsyncRule
{
    private const string UnitedKingdomIsoCode = "GB";
    private readonly ICountriesService _countriesService;
    private readonly IBcpService _bcpService;

    public TransitVerificationRule(ICountriesService countriesService, IBcpService bcpService)
    {
        _countriesService = countriesService;
        _bcpService = bcpService;
    }

    public bool ShouldInvoke(SpsCertificate spsCertificate)
    {
        var chedType = SpsCertificateHelper.GetChedType(spsCertificate.SpsExchangedDocument.IncludedSpsNote);
        var purpose = PurposeHelper.GetPurpose(spsCertificate.SpsExchangedDocument.SignatorySpsAuthentication);

        return chedType == ChedType.Chedp && purpose == Purpose.DirectTransit;
    }

    public async Task InvokeAsync(SpsCertificate spsCertificate, IList<ValidationError> validationErrors)
    {
        var chedType = SpsCertificateHelper.GetChedType(spsCertificate.SpsExchangedDocument.IncludedSpsNote)!;

        await ValidateThirdCountry(spsCertificate.SpsConsignment.ImportSpsCountry, validationErrors);
        await ValidateTransitingCountries(spsCertificate.SpsConsignment.TransitSpsCountry, validationErrors);
        await ValidateExitBcp(spsCertificate.SpsConsignment.TransitSpsCountry, chedType, validationErrors);
    }

    private async Task ValidateThirdCountry(SpsCountryType spsCountryType, IList<ValidationError> validationErrors)
    {
        var result = await _countriesService.GetCountryOrRegionByIsoCode(spsCountryType.Id.Value);

        if (!result.IsSuccess)
        {
            var validationError = result.StatusCode == HttpStatusCode.NotFound
                ? new ValidationError(RuleErrorMessage.ThirdCountryNotFound, RuleErrorId.ThirdCountryNotFound)
                : new ValidationError(RuleErrorMessage.CountriesClientError, RuleErrorId.CountriesClientError);

            validationErrors.Add(validationError);
        }
    }

    private async Task ValidateTransitingCountries(IList<SpsCountryType> spsCountryTypes, IList<ValidationError> validationErrors)
    {
        var isoCodes = spsCountryTypes
            .Select(x => x.Id.Value)
            .Where(x => x != UnitedKingdomIsoCode);

        foreach (var isoCode in isoCodes)
        {
            var result = await _countriesService.GetCountryOrRegionByIsoCode(isoCode);

            if (!result.IsSuccess)
            {
                var validationError = result.StatusCode == HttpStatusCode.NotFound
                    ? new ValidationError(string.Format(RuleErrorMessage.TransitingCountryNotFound, isoCode), RuleErrorId.TransitingCountryNotFound)
                    : new ValidationError(RuleErrorMessage.CountriesClientError, RuleErrorId.CountriesClientError);

                validationErrors.Add(validationError);
            }
        }
    }

    private async Task ValidateExitBcp(IList<SpsCountryType> spsCountryTypes, string chedType, IList<ValidationError> validationErrors)
    {
        var bcpCode = spsCountryTypes
            .Where(x => x.Id.Value == UnitedKingdomIsoCode)
            .SelectMany(x => x.SubordinateSpsCountrySubDivision)
            .SelectMany(x => x.ActivityAuthorizedSpsParty)
            .First(x => x.Id is not null).Id!.Value;

        var result = await _bcpService.GetBcpsWithCodeAndType(bcpCode, ChedType.ToDashed(chedType));

        if (!result.IsSuccess)
        {
            validationErrors.Add(new ValidationError(string.Format(RuleErrorMessage.BcpServiceError, bcpCode), RuleErrorId.BcpServiceError));

            return;
        }

        if (result.Value!.Count == 0)
        {
            validationErrors.Add(new ValidationError(RuleErrorMessage.ExitBcpNotFound, RuleErrorId.ExitBcpNotFound));

            return;
        }

        if (result.Value!.First().Suspended)
        {
            validationErrors.Add(new ValidationError(string.Format(RuleErrorMessage.BcpSuspended, bcpCode, chedType), RuleErrorId.BcpSuspended));
        }
    }
}
