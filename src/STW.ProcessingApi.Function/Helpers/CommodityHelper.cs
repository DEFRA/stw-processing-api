namespace STW.ProcessingApi.Function.Helpers;

using Constants;
using Models;

public static class CommodityHelper
{
    public static string? GetCommodityId(IList<ApplicableSpsClassification> applicableSpsClassifications)
    {
        return SpsCertificateHelper.GetApplicableSpsClassificationBySystemId(applicableSpsClassifications, SystemId.Cn)
            ?.ClassCode
            ?.Value;
    }

    public static string? GetEppoCode(IList<ApplicableSpsClassification> applicableSpsClassifications)
    {
        return SpsCertificateHelper.GetApplicableSpsClassificationBySystemId(applicableSpsClassifications, SystemId.Eppo)
            ?.ClassCode
            ?.Value;
    }
}
