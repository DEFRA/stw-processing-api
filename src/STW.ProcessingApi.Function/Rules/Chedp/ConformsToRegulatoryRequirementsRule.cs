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

    public void Invoke(SpsCertificate spsCertificate, IList<ValidationError> validationErrors)
    {
        var spsNoteType = SpsCertificateHelper.GetSpsNoteTypeBySubjectCode(spsCertificate.SpsExchangedDocument.IncludedSpsNote, SubjectCode.ConformsToEu);

        ValidateSpsNoteType(spsNoteType, validationErrors);
    }

    private void ValidateSpsNoteType(SpsNoteType? spsNoteType, IList<ValidationError> validationErrors)
    {
        if (spsNoteType is null)
        {
            validationErrors.Add(new ValidationError(RuleErrorMessage.ConformsToEuNoteNotFound, RuleErrorId.ConformsToEuNoteNotFound));
        }
        else if (!_allowedValues.Contains(spsNoteType.Content[0].Value))
        {
            validationErrors.Add(new ValidationError(RuleErrorMessage.ConformsToEuNoteInvalidValue, RuleErrorId.ConformsToEuNoteInvalidValue));
        }
    }
}
