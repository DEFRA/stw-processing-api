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

    public void Invoke(SpsCertificate spsCertificate, IList<ValidationError> validationErrors)
    {
        spsCertificate.SpsConsignment
            .IncludedSpsConsignmentItem
            .First()
            .IncludedSpsTradeLineItem
            .ToList()
            .ForEach(x => ValidateTradeLineItem(x, validationErrors));
    }

    private static void ValidateTradeLineItem(IncludedSpsTradeLineItem tradeLineItem, IList<ValidationError> validationErrors)
    {
        var sequenceNumericNumber = tradeLineItem.SequenceNumeric.Value;
        var variety = ComplementHelper.GetVariety(tradeLineItem.AdditionalInformationSpsNote);
        var clazz = ComplementHelper.GetClass(tradeLineItem.AdditionalInformationSpsNote);
        var hasInvalidCombination = false;

        if (variety is not null && clazz is null)
        {
            hasInvalidCombination = true;
            validationErrors.Add(new ValidationError(RuleErrorMessage.ClassMissing, RuleErrorId.ClassMissing, sequenceNumericNumber));
        }

        if (variety is null && clazz is not null)
        {
            hasInvalidCombination = true;
            validationErrors.Add(new ValidationError(RuleErrorMessage.VarietyMissing, RuleErrorId.VarietyMissing, sequenceNumericNumber));
        }

        if (!hasInvalidCombination && string.IsNullOrEmpty(variety))
        {
            validationErrors.Add(new ValidationError(RuleErrorMessage.VarietyEmpty, RuleErrorId.VarietyEmpty, sequenceNumericNumber));
        }

        if (!hasInvalidCombination && string.IsNullOrEmpty(clazz))
        {
            validationErrors.Add(new ValidationError(RuleErrorMessage.ClassEmpty, RuleErrorId.ClassEmpty, sequenceNumericNumber));
        }
    }
}
