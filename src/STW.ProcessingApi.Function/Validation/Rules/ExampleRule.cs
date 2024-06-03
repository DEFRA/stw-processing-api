namespace STW.ProcessingApi.Function.Validation.Rules;

using Models;

public class ExampleRule : Rule
{
    private const string NoScientificName = "Trade line item is missing a scientific name";
    private const string NoEppoCode = "Trade line item is missing an EPPO code";
    private const int ErrorId = 4;

    public override List<ValidationError> Validate(SpsCertificate spsCertificate)
    {
        var tradeLineItems = spsCertificate.SpsConsignment.IncludedSpsConsignmentItem.First().IncludedSpsTradeLineItem;
        foreach (var tradeLineItem in tradeLineItems)
        {
            var sequenceNumeric = (int)tradeLineItem.SequenceNumeric.Value;

            if (!HasScientificName(tradeLineItem))
            {
                Errors.Add(new ValidationError(NoScientificName, ErrorId, sequenceNumeric));
            }

            if (!HasEppoCode(tradeLineItem))
            {
                Errors.Add(new ValidationError(NoEppoCode, ErrorId, sequenceNumeric));
            }

            tradeLineItem.ScientificName.Add(
                new TextType()
                {
                    Value = "Test",
                });
        }

        return Errors;
    }

    private bool HasScientificName(IncludedSpsTradeLineItem tradeLineItem) // This check is already done in the schema
    {
        return tradeLineItem.ScientificName.Count > 0 &&
               !string.IsNullOrWhiteSpace(tradeLineItem.ScientificName.First().Value);
    }

    private bool HasEppoCode(IncludedSpsTradeLineItem tradeLineItem) // The empty string check can go in schema
    {
        var classifications = tradeLineItem.ApplicableSpsClassification;
        return classifications.Any(
            classification => classification.SystemID.Value.Equals("EPPO") &&
                              !string.IsNullOrWhiteSpace(classification.ClassCode.Value));
    }
}
