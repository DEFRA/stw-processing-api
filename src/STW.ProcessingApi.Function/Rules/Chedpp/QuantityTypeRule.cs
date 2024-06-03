namespace STW.ProcessingApi.Function.Rules.Chedpp;

using Constants;
using Helpers;
using Interfaces;
using Models;

public class QuantityTypeRule : IRule
{
    private static readonly Dictionary<string, string> _quantityTypes = new Dictionary<string, string>
    {
        { "STM", "Stems" },
        { "BLB", "Bulbs" },
        { "CRZ", "Corms and rhizomes" },
        { "PTC", "Plants in tissue culture" },
        { "SDS", "Seeds" },
        { "KGM", "Kilograms" },
        { "PCS", "Pieces" }
    };

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
            .ForEach(x => ValidateQuantityType(x, validationErrors));
    }

    private static void ValidateQuantityType(IncludedSpsTradeLineItem tradeLineItem, IList<ValidationError> validationErrors)
    {
        var sequenceNumericNumber = tradeLineItem.SequenceNumeric.Value;

        var spsNoteType = tradeLineItem.AdditionalInformationSpsNote
            .FirstOrDefault(x => x.SubjectCode?.Value == SubjectCode.Quantity);

        if (spsNoteType is null)
        {
            validationErrors.Add(new ValidationError(RuleErrorMessage.MissingQuantityType, RuleErrorId.MissingQuantityType, sequenceNumericNumber));

            return;
        }

        if (!IsValidQuantityType(spsNoteType))
        {
            validationErrors.Add(new ValidationError(RuleErrorMessage.InvalidQuantityType, RuleErrorId.InvalidQuantityType, sequenceNumericNumber));

            return;
        }

        if (GetQuantity(spsNoteType) <= 0)
        {
            validationErrors.Add(new ValidationError(RuleErrorMessage.QuantityMustBeOneOrMore, RuleErrorId.QuantityMustBeOneOrMore, sequenceNumericNumber));
        }
    }

    private static bool IsValidQuantityType(SpsNoteType spsNoteType)
    {
        var hasExpectedContentCode = spsNoteType.ContentCode.Count == 1 && _quantityTypes.ContainsKey(spsNoteType.ContentCode[0].Value);
        var hasExpectedContent = spsNoteType.Content.Count == 1 && double.TryParse(spsNoteType.Content[0].Value, out _);

        return hasExpectedContentCode && hasExpectedContent;
    }

    private static double GetQuantity(SpsNoteType spsNoteType)
    {
        return spsNoteType.Content
            .Select(x => x.Value)
            .Where(x => double.TryParse(x, out _))
            .Select(double.Parse)
            .FirstOrDefault();
    }
}
