namespace STW.ProcessingApi.Function.Rules.Chedp;

using Constants;
using Helpers;
using Interfaces;
using Models;

public class ReEntryRule : IRule
{
    public bool ShouldInvoke(SpsCertificate spsCertificate)
    {
        var chedType = SpsCertificateHelper.GetChedType(spsCertificate.SpsExchangedDocument.IncludedSpsNote);
        var purpose = PurposeHelper.GetPurpose(spsCertificate.SpsExchangedDocument.SignatorySpsAuthentication);

        return chedType == ChedType.Chedp && purpose == Purpose.ReEntry;
    }

    public void Invoke(SpsCertificate spsCertificate, IList<ErrorEvent> errorEvents)
    {
        if (!PurposeHelper.ConsignmentConformsToEu(spsCertificate.SpsExchangedDocument.IncludedSpsNote))
        {
            errorEvents.Add(new ErrorEvent(RuleErrorMessage.ConformsToEuMustBeTrueForReEntry));
        }
    }
}
