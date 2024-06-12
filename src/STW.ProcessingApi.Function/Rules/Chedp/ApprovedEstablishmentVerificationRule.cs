namespace STW.ProcessingApi.Function.Rules.Chedp;

using Constants;
using Helpers;
using Interfaces;
using Models;
using Models.ApprovedEstablishment;
using Services.Interfaces;

public class ApprovedEstablishmentVerificationRule : IAsyncRule
{
    private readonly IApprovedEstablishmentService _approvedEstablishmentService;

    public ApprovedEstablishmentVerificationRule(IApprovedEstablishmentService approvedEstablishmentService)
    {
        _approvedEstablishmentService = approvedEstablishmentService;
    }

    public bool ShouldInvoke(SpsCertificate spsCertificate)
    {
        var chedType = SpsCertificateHelper.GetChedType(spsCertificate.SpsExchangedDocument.IncludedSpsNote);

        return chedType == ChedType.Chedp;
    }

    public async Task InvokeAsync(SpsCertificate spsCertificate, IList<ValidationError> validationErrors)
    {
        var searchQueries = BuildSearchQueries(spsCertificate);

        foreach (var searchQuery in searchQueries)
        {
            var result = await _approvedEstablishmentService.Search(searchQuery);

            if (!result.IsSuccess)
            {
                validationErrors.Add(new ValidationError(RuleErrorMessage.ApprovedEstablishmentClientError, RuleErrorId.ApprovedEstablishmentClientError));

                return;
            }

            if (result.Value!.TotalElements == 0)
            {
                validationErrors.Add(
                    new ValidationError(
                        string.Format(RuleErrorMessage.ApprovedEstablishmentNotFound, searchQuery.ApprovalNumber),
                        RuleErrorId.ApprovedEstablishmentNotFound));
            }
        }
    }

    private static IEnumerable<ApprovedEstablishmentSearchQuery> BuildSearchQueries(SpsCertificate spsCertificate)
    {
        return spsCertificate.SpsConsignment
            .IncludedSpsConsignmentItem
            .First()
            .IncludedSpsTradeLineItem
            .SelectMany(x => x.AppliedSpsProcess)
            .Select(BuildSearchQuery);
    }

    private static ApprovedEstablishmentSearchQuery BuildSearchQuery(AppliedSpsProcess appliedSpsProcess)
    {
        var spsPartyType = appliedSpsProcess.OperatorSpsParty!;
        var spsCountryType = appliedSpsProcess.OperationSpsCountry!;

        return new ApprovedEstablishmentSearchQuery
        {
            ApprovalNumber = spsPartyType.Id!.Value,
            ActivitiesLegend = appliedSpsProcess.TypeCode.Name,
            CountryCode = spsCountryType.Id.Value,
            Section = spsPartyType.RoleCode!.Name
        };
    }
}
