namespace STW.ProcessingApi.Function.Helpers;

using Constants;
using Models;

public static class ComplementHelper
{
    public static string? GetVariety(IList<SpsNoteType> spsNoteTypes)
    {
        return SpsCertificateHelper.GetSpsNoteTypeContentValueBySubjectCode(spsNoteTypes, SubjectCode.Variety);
    }

    public static string? GetClass(IList<SpsNoteType> spsNoteTypes)
    {
        return SpsCertificateHelper.GetSpsNoteTypeContentValueBySubjectCode(spsNoteTypes, SubjectCode.Class);
    }

    public static string? GetFinishedOrPropagated(IList<SpsNoteType> spsNoteTypes)
    {
        return SpsCertificateHelper.GetSpsNoteTypeContentValueBySubjectCode(spsNoteTypes, SubjectCode.FinishedOrPropagated);
    }

    public static string? GetSpeciesTypeName(IList<ApplicableSpsClassification> applicableSpsClassifications)
    {
        var applicableSpsClassification = SpsCertificateHelper.GetApplicableSpsClassificationBySystemName(applicableSpsClassifications, SystemName.IpaffsCct);

        return applicableSpsClassification?.ClassName.FirstOrDefault()?.Value;
    }

    public static string? GetSpeciesClassName(IList<ApplicableSpsClassification> applicableSpsClassifications)
    {
        var applicableSpsClassification = SpsCertificateHelper.GetApplicableSpsClassificationBySystemName(applicableSpsClassifications, SystemName.IpaffsCcc);

        return applicableSpsClassification?.ClassName.FirstOrDefault()?.Value;
    }

    public static string? GetSpeciesFamilyName(IList<ApplicableSpsClassification> applicableSpsClassifications)
    {
        var applicableSpsClassification = SpsCertificateHelper.GetApplicableSpsClassificationBySystemName(applicableSpsClassifications, SystemName.IpaffsCcf);

        return applicableSpsClassification?.ClassName.FirstOrDefault()?.Value;
    }
}
