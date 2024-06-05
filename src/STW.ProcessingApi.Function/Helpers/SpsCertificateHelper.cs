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
}
