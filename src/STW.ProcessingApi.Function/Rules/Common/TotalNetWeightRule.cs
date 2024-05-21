namespace STW.ProcessingApi.Function.Rules.Common;

using Constants;
using Extensions;
using Helpers;
using Interfaces;
using Models;

public class TotalNetWeightRule : IRule
{
    private const double MinWeight = 0.001;
    private const int MaxScale = 3;
    private const int MaxPrecision = 16;
    private readonly List<string> _chedTypes = [ChedType.Chedpp, ChedType.Chedp];

    public bool ShouldInvoke(SpsCertificate spsCertificate)
    {
        var chedType = SpsCertificateHelper.GetChedType(spsCertificate.SpsExchangedDocument.IncludedSpsNote);

        return chedType is not null && _chedTypes.Contains(chedType);
    }

    public void Invoke(SpsCertificate spsCertificate, IList<ErrorEvent> errorEvents)
    {
        spsCertificate.SpsConsignment.IncludedSpsConsignmentItem
            .First()
            .IncludedSpsTradeLineItem
            .Where(x => x.NetWeightMeasure is not null)
            .SelectMany(x => ValidateNetWeight(x.SequenceNumeric.Value, x.NetWeightMeasure!.Value))
            .ToList()
            .ForEach(errorEvents.Add);
    }

    private static List<ErrorEvent> ValidateNetWeight(int sequenceNumeric, double netWeight)
    {
        var errorEvents = new List<ErrorEvent>();

        if (netWeight < MinWeight)
        {
            errorEvents.Add(new ErrorEvent(RuleErrorMessage.NetWeightLessThanMinWeight, RuleErrorId.NetWeightLessThanMinWeight, sequenceNumeric));
        }

        var netWeightDecimal = (decimal)netWeight;

        if (netWeightDecimal.RemoveTrailingZeros().Scale > MaxScale)
        {
            errorEvents.Add(new ErrorEvent(RuleErrorMessage.NetWeightTooManyDecimals, RuleErrorId.NetWeightTooManyDecimals, sequenceNumeric));
        }

        if (netWeightDecimal.Precision() > MaxPrecision)
        {
            errorEvents.Add(new ErrorEvent(RuleErrorMessage.NetWeightTooManyDigits, RuleErrorId.NetWeightTooManyDigits, sequenceNumeric));
        }

        return errorEvents;
    }
}
