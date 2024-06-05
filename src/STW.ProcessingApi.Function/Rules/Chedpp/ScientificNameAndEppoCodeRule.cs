namespace STW.ProcessingApi.Function.Rules.Chedpp;

using Constants;
using Helpers;
using Interfaces;
using Models;

public class ScientificNameAndEppoCodeRule : IRule
{
    public bool ShouldInvoke(SpsCertificate spsCertificate)
    {
        var chedType = SpsCertificateHelper.GetChedType(spsCertificate.SpsExchangedDocument.IncludedSpsNote);

        return chedType == ChedType.Chedpp;
    }

    public void Invoke(SpsCertificate spsCertificate, IList<ValidationError> validationErrors)
    {
        spsCertificate.SpsConsignment
            .IncludedSpsConsignmentItem
            .First()
            .IncludedSpsTradeLineItem
            .Where(x => !HasScientificName(x))
            .Where(x => !HasEppoCode(x))
            .Select(x => x.SequenceNumeric.Value)
            .Select(x => new ValidationError(string.Format(RuleErrorMessage.ScientificNameOrEppoCodeMissing, x), RuleErrorId.ScientificNameOrEppoCodeMissing, x))
            .ToList()
            .ForEach(validationErrors.Add);
    }

    private static bool HasScientificName(IncludedSpsTradeLineItem tradeLineItem)
        => tradeLineItem.ScientificName.Count > 0 && !string.IsNullOrEmpty(tradeLineItem.ScientificName[0].Value);

    private static bool HasEppoCode(IncludedSpsTradeLineItem tradeLineItem)
        => tradeLineItem.ApplicableSpsClassification.Any(
            x => x.SystemId?.Value == CommodityComplementKey.Eppo && !string.IsNullOrEmpty(x.ClassCode?.Value));
}
