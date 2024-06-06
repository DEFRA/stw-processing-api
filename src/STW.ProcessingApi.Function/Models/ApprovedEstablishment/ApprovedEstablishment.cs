namespace STW.ProcessingApi.Function.Models.ApprovedEstablishment;

public class ApprovedEstablishment
{
    public string Id { get; init; }

    public string Country { get; init; }

    public string CountryCode { get; init; }

    public string Section { get; init; }

    public string ApprovalNumber { get; init; }

    public string CompanyName { get; init; }

    public string City { get; init; }

    public string Region { get; init; }

    public string Activities { get; init; }

    public string Remarks { get; init; }

    public string DateOfRequest { get; init; }

    public string DateOfApproval { get; init; }

    public string ValidityDateFrom { get; init; }

    public string ActivitiesLegend { get; init; }

    public string RemarksLegend { get; init; }

    public string Status { get; init; }

    public string DateOfLastStatusChange { get; init; }

    public bool IsDeleted { get; init; }
}
