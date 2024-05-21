namespace STW.ProcessingApi.Function.Rules.Chedp;

using Constants;
using Helpers;
using Interfaces;
using Models;

public class ConformsToRegulatoryRequirementsRule : IRule
{
    private readonly List<string> _allowedValues = ["TRUE", "FALSE"];

    public bool ShouldInvoke(SpsCertificate spsCertificate)
    {
        var chedType = SpsCertificateHelper.GetChedType(spsCertificate.SpsExchangedDocument.IncludedSpsNote);

        return chedType == ChedType.Chedp;
    }

    public void Invoke(SpsCertificate spsCertificate, IList<ErrorEvent> errorEvents)
    {
        var spsNoteType = SpsCertificateHelper.GetSpsNoteTypeBySubjectCode(spsCertificate.SpsExchangedDocument.IncludedSpsNote, SubjectCode.ConformsToEu);

        ValidateSpsNoteType(spsNoteType, errorEvents);
    }

    private void ValidateSpsNoteType(SpsNoteType? spsNoteType, IList<ErrorEvent> errorEvents)
    {
        if (spsNoteType is null)
        {
            errorEvents.Add(new ErrorEvent(RuleErrorMessage.ConformsToEuNoteNotFound));
        }
        else if (!_allowedValues.Contains(spsNoteType.Content[0].Value))
        {
            errorEvents.Add(new ErrorEvent(RuleErrorMessage.ConformsToEuNoteInvalidValue));
        }
    }
}
