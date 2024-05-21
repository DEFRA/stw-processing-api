namespace STW.ProcessingApi.Function.Rules.Chedpp;

using Constants;
using Helpers;
using Interfaces;
using Models;

public class ChedppInternalMarketRule : IRule
{
    public bool ShouldInvoke(SpsCertificate spsCertificate)
    {
        var chedType = SpsCertificateHelper.GetChedType(spsCertificate.SpsExchangedDocument.IncludedSpsNote);

        return chedType == ChedType.Chedpp;
    }

    public void Invoke(SpsCertificate spsCertificate, IList<ErrorEvent> errorEvents)
    {
        var purpose = PurposeHelper.GetPurpose(spsCertificate.SpsExchangedDocument.SignatorySpsAuthentication);

        if (purpose != Purpose.InternalMarket)
        {
            errorEvents.Add(new ErrorEvent(RuleErrorMessage.PurposeMustBeInternalMarket));
        }
    }
}
