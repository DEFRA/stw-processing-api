namespace STW.ProcessingApi.Function.Helpers;

using Constants;
using Models;

public class ComplementHelper
{
    public static string? GetVariety(IList<SpsNoteType> spsNoteTypes)
    {
        return SpsCertificateHelper.GetSpsNoteTypeContentValueBySubjectCode(spsNoteTypes, SubjectCode.Variety);
    }

    public static string? GetClass(IList<SpsNoteType> spsNoteTypes)
    {
        return SpsCertificateHelper.GetSpsNoteTypeContentValueBySubjectCode(spsNoteTypes, SubjectCode.Class);
    }
}
