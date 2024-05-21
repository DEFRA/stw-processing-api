namespace STW.ProcessingApi.Function.Helpers;

using Constants;
using Models;

public static class SpsCertificateHelper
{
    public static string? GetSpsNoteTypeContentValueBySubjectCode(IList<SpsNoteType> spsNoteTypes, string subjectCode)
    {
        return GetSpsNoteTypeBySubjectCode(spsNoteTypes, subjectCode)?.Content[0].Value;
    }

    public static SpsNoteType? GetSpsNoteTypeBySubjectCode(IList<SpsNoteType> spsNoteTypes, string subjectCode)
    {
        return spsNoteTypes
            .FirstOrDefault(x => x.SubjectCode?.Value == subjectCode);
    }

    public static string? GetChedType(IList<SpsNoteType> spsNoteTypes)
    {
        return GetSpsNoteTypeBySubjectCode(spsNoteTypes, SubjectCode.ChedType)?.Content.FirstOrDefault()?.Value;
    }

    public static ApplicableSpsClassification? GetApplicableSpsClassificationBySystemId(IList<ApplicableSpsClassification> applicableSpsClassifications, string systemId)
    {
        return applicableSpsClassifications.FirstOrDefault(x => x.SystemId?.Value == systemId);
    }

    public static ApplicableSpsClassification? GetApplicableSpsClassificationBySystemName(IList<ApplicableSpsClassification> applicableSpsClassifications, string systemName)
    {
        return applicableSpsClassifications.FirstOrDefault(x => x.SystemName.Any(z => z.Value == systemName));
    }
}
