namespace STW.ProcessingApi.Function.Rules.Common;

using Constants;
using Helpers;
using Interfaces;
using Models;

public class TradeLineItemSequenceRule : IRule
{
    private readonly List<string> _chedTypes = [ChedType.Chedpp, ChedType.Chedp];

    public bool ShouldInvoke(SpsCertificate spsCertificate)
    {
        var chedType = SpsCertificateHelper.GetChedType(spsCertificate.SpsExchangedDocument.IncludedSpsNote);

        return chedType is not null && _chedTypes.Contains(chedType);
    }

    public void Invoke(SpsCertificate spsCertificate, IList<ValidationError> validationErrors)
    {
        var sequence = spsCertificate.SpsConsignment
            .IncludedSpsConsignmentItem
            .First()
            .IncludedSpsTradeLineItem
            .Select(x => x.SequenceNumeric.Value)
            .ToList();

        ValidateSequenceOrder(sequence).ForEach(validationErrors.Add);
    }

    private static List<ValidationError> ValidateSequenceOrder(List<int> sequence)
    {
        return Enumerable.Range(0, sequence.Count)
            .Where(x => sequence[x] != x + 1)
            .Select(x => new ValidationError(RuleErrorMessage.SequenceNumericOrder, RuleErrorId.SequenceNumericOrder, sequence[x]))
            .ToList();
    }
}
