namespace STW.ProcessingApi.Function.Rules.Chedp;

using Constants;
using Helpers;
using Interfaces;
using Models;

public class TranshipmentRule : IRule
{
    public bool ShouldInvoke(SpsCertificate spsCertificate)
    {
        var chedType = SpsCertificateHelper.GetChedType(spsCertificate.SpsExchangedDocument.IncludedSpsNote);
        var purpose = PurposeHelper.GetPurpose(spsCertificate.SpsExchangedDocument.SignatorySpsAuthentication);

        return chedType == ChedType.Chedp && purpose == Purpose.Transhipment;
    }

    public void Invoke(SpsCertificate spsCertificate, IList<ValidationError> validationErrors)
    {
        if (string.IsNullOrEmpty(spsCertificate.SpsConsignment.ImportSpsCountry.Id.Value))
        {
            validationErrors.Add(new ValidationError(RuleErrorMessage.MissingImportSpsCountry, RuleErrorId.MissingImportSpsCountry));
        }

        if (!IsValidFinalBcp(spsCertificate.SpsConsignment.TransitSpsCountry))
        {
            validationErrors.Add(new ValidationError(RuleErrorMessage.MissingFinalBcp, RuleErrorId.MissingFinalBcp));
        }
    }

    private static bool IsValidFinalBcp(IList<SpsCountryType> spsCountryTypes)
    {
        return spsCountryTypes
            .SelectMany(x => x.SubordinateSpsCountrySubDivision)
            .SelectMany(x => x.ActivityAuthorizedSpsParty)
            .Any(x => x.Id is not null);
    }
}
