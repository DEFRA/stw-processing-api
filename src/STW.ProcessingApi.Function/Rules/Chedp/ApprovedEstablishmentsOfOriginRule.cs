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

    public void Invoke(SpsCertificate spsCertificate, IList<ErrorEvent> errorEvents)
    {
        spsCertificate.SpsConsignment
            .IncludedSpsConsignmentItem
            .First()
            .IncludedSpsTradeLineItem
            .Where(x => x.AppliedSpsProcess.Count > 0)
            .SelectMany(x => x.AppliedSpsProcess)
            .ToList()
            .ForEach(x => ValidateAppliedSpsProcess(x, errorEvents));
    }

    private static void ValidateAppliedSpsProcess(AppliedSpsProcess appliedSpsProcess, IList<ErrorEvent> errors)
    {
        ValidateOperationSpsCountry(appliedSpsProcess.OperationSpsCountry, errors);
        ValidateOperatorSpsParty(appliedSpsProcess.OperatorSpsParty, errors);
        ValidateTypeCode(appliedSpsProcess.TypeCode, errors);
    }

    private static void ValidateOperationSpsCountry(SpsCountryType? spsCountryType, IList<ErrorEvent> errorEvents)
    {
        if (string.IsNullOrEmpty(spsCountryType?.Id.Value))
        {
            errorEvents.Add(new ErrorEvent(RuleErrorMessage.ApprovedEstablishmentMissingCountryCode));
        }

        if (string.IsNullOrEmpty(spsCountryType?.Name.First().Value))
        {
            errorEvents.Add(new ErrorEvent(RuleErrorMessage.ApprovedEstablishmentMissingCountryName));
        }
    }

    private static void ValidateOperatorSpsParty(SpsPartyType? spsPartyType, IList<ErrorEvent> errorEvents)
    {
        if (string.IsNullOrEmpty(spsPartyType?.Id?.Value))
        {
            errorEvents.Add(new ErrorEvent(RuleErrorMessage.ApprovedEstablishmentMissingApprovalNumber));
        }

        if (string.IsNullOrEmpty(spsPartyType?.Name.Value))
        {
            errorEvents.Add(new ErrorEvent(RuleErrorMessage.ApprovedEstablishmentMissingOperatorName));
        }

        if (string.IsNullOrEmpty(spsPartyType?.RoleCode?.Name))
        {
            errorEvents.Add(new ErrorEvent(RuleErrorMessage.ApprovedEstablishmentMissingSectionValue));
        }
        else if (spsPartyType.RoleCode?.Value != RoleCodeValue.ZZZ)
        {
            errorEvents.Add(new ErrorEvent(RuleErrorMessage.ApprovedEstablishmentIncorrectRoleCode));
        }
    }

    private static void ValidateTypeCode(ProcessTypeCodeType typeCode, IList<ErrorEvent> errorEvents)
    {
        if (string.IsNullOrEmpty(typeCode.Name))
        {
            errorEvents.Add(new ErrorEvent(RuleErrorMessage.ApprovedEstablishmentMissingActivityName));
        }

        if (string.IsNullOrEmpty(typeCode.Value))
        {
            errorEvents.Add(new ErrorEvent(RuleErrorMessage.ApprovedEstablishmentMissingActivityCode));
        }
    }
}
