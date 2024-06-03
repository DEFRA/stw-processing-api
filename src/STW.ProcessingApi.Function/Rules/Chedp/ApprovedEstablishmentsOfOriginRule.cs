namespace STW.ProcessingApi.Function.Rules.Chedp;

using Constants;
using Helpers;
using Interfaces;
using Models;

public class ApprovedEstablishmentsOfOriginRule : IRule
{
    public bool ShouldInvoke(SpsCertificate spsCertificate)
    {
        var chedType = SpsCertificateHelper.GetChedType(spsCertificate.SpsExchangedDocument.IncludedSpsNote);

        return chedType == ChedType.Chedp;
    }

    public void Invoke(SpsCertificate spsCertificate, IList<ValidationError> validationErrors)
    {
        spsCertificate.SpsConsignment
            .IncludedSpsConsignmentItem
            .First()
            .IncludedSpsTradeLineItem
            .Where(x => x.AppliedSpsProcess.Count > 0)
            .SelectMany(x => x.AppliedSpsProcess)
            .ToList()
            .ForEach(x => ValidateAppliedSpsProcess(x, validationErrors));
    }

    private static void ValidateAppliedSpsProcess(AppliedSpsProcess appliedSpsProcess, IList<ValidationError> errors)
    {
        ValidateOperationSpsCountry(appliedSpsProcess.OperationSpsCountry, errors);
        ValidateOperatorSpsParty(appliedSpsProcess.OperatorSpsParty, errors);
        ValidateTypeCode(appliedSpsProcess.TypeCode, errors);
    }

    private static void ValidateOperationSpsCountry(SpsCountryType? spsCountryType, IList<ValidationError> validationErrors)
    {
        if (string.IsNullOrEmpty(spsCountryType?.Id.Value))
        {
            validationErrors.Add(
                new ValidationError(
                    RuleErrorMessage.ApprovedEstablishmentMissingCountryCode,
                    RuleErrorId.ApprovedEstablishmentMissingCountryCode));
        }

        if (string.IsNullOrEmpty(spsCountryType?.Name.First().Value))
        {
            validationErrors.Add(
                new ValidationError(
                    RuleErrorMessage.ApprovedEstablishmentMissingCountryName,
                    RuleErrorId.ApprovedEstablishmentMissingCountryName));
        }
    }

    private static void ValidateOperatorSpsParty(SpsPartyType? spsPartyType, IList<ValidationError> validationErrors)
    {
        if (string.IsNullOrEmpty(spsPartyType?.Id?.Value))
        {
            validationErrors.Add(
                new ValidationError(
                    RuleErrorMessage.ApprovedEstablishmentMissingApprovalNumber,
                    RuleErrorId.ApprovedEstablishmentMissingApprovalNumber));
        }

        if (string.IsNullOrEmpty(spsPartyType?.Name.Value))
        {
            validationErrors.Add(
                new ValidationError(
                    RuleErrorMessage.ApprovedEstablishmentMissingOperatorName,
                    RuleErrorId.ApprovedEstablishmentMissingOperatorName));
        }

        if (string.IsNullOrEmpty(spsPartyType?.RoleCode?.Name))
        {
            validationErrors.Add(
                new ValidationError(
                    RuleErrorMessage.ApprovedEstablishmentMissingSection,
                    RuleErrorId.ApprovedEstablishmentMissingSection));
        }
        else if (spsPartyType.RoleCode?.Value != RoleCodeValue.ZZZ)
        {
            validationErrors.Add(
                new ValidationError(
                    RuleErrorMessage.ApprovedEstablishmentIncorrectRoleCode,
                    RuleErrorId.ApprovedEstablishmentIncorrectRoleCode));
        }
    }

    private static void ValidateTypeCode(ProcessTypeCodeType typeCode, IList<ValidationError> validationErrors)
    {
        if (string.IsNullOrEmpty(typeCode.Name))
        {
            validationErrors.Add(
                new ValidationError(
                    RuleErrorMessage.ApprovedEstablishmentMissingActivityName,
                    RuleErrorId.ApprovedEstablishmentMissingActivityName));
        }

        if (string.IsNullOrEmpty(typeCode.Value))
        {
            validationErrors.Add(
                new ValidationError(
                    RuleErrorMessage.ApprovedEstablishmentMissingActivityCode,
                    RuleErrorId.ApprovedEstablishmentMissingActivityCode));
        }
    }
}
