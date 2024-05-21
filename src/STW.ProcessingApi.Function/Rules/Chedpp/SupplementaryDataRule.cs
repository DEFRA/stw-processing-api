namespace STW.ProcessingApi.Function.Rules.Chedpp;

using Constants;
using Helpers;
using Interfaces;
using Models;

public class SupplementaryDataRule : IRule
{
    public bool ShouldInvoke(SpsCertificate spsCertificate)
    {
        var chedType = SpsCertificateHelper.GetChedType(spsCertificate.SpsExchangedDocument.IncludedSpsNote);

        return chedType == ChedType.Chedpp;
    }

    public void Invoke(SpsCertificate spsCertificate, IList<ErrorEvent> errorEvents)
    {
        spsCertificate.SpsConsignment
            .IncludedSpsConsignmentItem
            .First()
            .IncludedSpsTradeLineItem
            .ToList()
            .ForEach(x => ValidateTradeLineItem(x, errorEvents));
    }

    private static void ValidateTradeLineItem(IncludedSpsTradeLineItem tradeLineItem, IList<ErrorEvent> errorEvents)
    {
        var sequenceNumericNumber = tradeLineItem.SequenceNumeric.Value;
        var variety = GetVariety(tradeLineItem);
        var clazz = GetClass(tradeLineItem);
        var hasInvalidCombination = false;

        if (variety is not null && clazz is null)
        {
            hasInvalidCombination = true;
            errorEvents.Add(new ErrorEvent(RuleErrorMessage.ClassMissing, RuleErrorId.ClassMissing, sequenceNumericNumber));
        }

        if (variety is null && clazz is not null)
        {
            hasInvalidCombination = true;
            errorEvents.Add(new ErrorEvent(RuleErrorMessage.VarietyMissing, RuleErrorId.VarietyMissing, sequenceNumericNumber));
        }

        if (!hasInvalidCombination && string.IsNullOrEmpty(variety))
        {
            errorEvents.Add(new ErrorEvent(RuleErrorMessage.VarietyEmpty, RuleErrorId.VarietyEmpty, sequenceNumericNumber));
        }

        if (!hasInvalidCombination && string.IsNullOrEmpty(clazz))
        {
            errorEvents.Add(new ErrorEvent(RuleErrorMessage.ClassEmpty, RuleErrorId.ClassEmpty, sequenceNumericNumber));
        }
    }

    private static string? GetVariety(IncludedSpsTradeLineItem tradeLineItem)
    {
        return tradeLineItem.AdditionalInformationSpsNote
            .FirstOrDefault(x => x.SubjectCode?.Value == SubjectCode.Variety)
            ?.Content[0].Value;
    }

    private static string? GetClass(IncludedSpsTradeLineItem tradeLineItem)
    {
        return tradeLineItem.AdditionalInformationSpsNote
            .FirstOrDefault(x => x.SubjectCode?.Value == SubjectCode.Class)
            ?.Content[0].Value;
    }
}
