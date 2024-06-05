namespace STW.ProcessingApi.Function.Helpers;

using Constants;
using Models;

public static class PurposeHelper
{
    public static bool ConsignmentConformsToEu(IList<SpsNoteType> spsNoteTypes)
    {
        var value = SpsCertificateHelper.GetSpsNoteTypeContentValueBySubjectCode(spsNoteTypes, SubjectCode.ConformsToEu);

        return bool.TryParse(value, out var boolean) && boolean;
    }

    public static string? GetNonConformingGoodsDestinationRegisteredNumber(IList<SpsNoteType> spsNoteTypes)
    {
        return SpsCertificateHelper.GetSpsNoteTypeContentValueBySubjectCode(spsNoteTypes, SubjectCode.NonConformingGoodsDestinationRegisteredNumber);
    }

    public static string? GetNonConformingGoodsDestinationShipName(IList<SpsNoteType> spsNoteTypes)
    {
        return SpsCertificateHelper.GetSpsNoteTypeContentValueBySubjectCode(spsNoteTypes, SubjectCode.NonConformingGoodsDestinationShipName);
    }

    public static string? GetNonConformingGoodsDestinationShipPort(IList<SpsNoteType> spsNoteTypes)
    {
        return SpsCertificateHelper.GetSpsNoteTypeContentValueBySubjectCode(spsNoteTypes, SubjectCode.NonConformingGoodsDestinationShipPort);
    }

    public static string? GetNonConformingGoodsDestinationType(IList<SpsNoteType> spsNoteTypes)
    {
        return SpsCertificateHelper.GetSpsNoteTypeContentValueBySubjectCode(spsNoteTypes, SubjectCode.NonConformingGoodsDestinationType);
    }

    public static string? GetGoodsCertifiedAs(IList<SpsAuthenticationType> spsAuthenticationTypes)
    {
        var includedSpsClause = spsAuthenticationTypes
            .SelectMany(x => x.IncludedSpsClause)
            .FirstOrDefault(x => x.Id.Value == SubjectCode.GoodsCertifiedAs);

        return includedSpsClause?.Content[0].Value;
    }

    public static string? GetPurpose(IList<SpsAuthenticationType> spsAuthenticationTypes)
    {
        var includedSpsClause = spsAuthenticationTypes
            .SelectMany(x => x.IncludedSpsClause)
            .FirstOrDefault(x => x.Id.Value == SubjectCode.Purpose);

        return includedSpsClause?.Content[0].Value;
    }
}
