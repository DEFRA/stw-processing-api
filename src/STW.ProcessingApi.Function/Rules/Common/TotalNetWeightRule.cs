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
    private readonly string[] _chedTypes = new string[] { ChedType.Chedp, ChedType.Chedpp };

    public bool ShouldInvoke(SpsCertificate spsCertificate)
    {
        var chedType = SpsCertificateHelper.GetChedType(spsCertificate.SpsExchangedDocument.IncludedSpsNote);

        return chedType is not null && _chedTypes.Contains(chedType);
    }

    public void Invoke(SpsCertificate spsCertificate, IList<ValidationError> validationErrors)
    {
        spsCertificate.SpsConsignment.IncludedSpsConsignmentItem
            .First()
            .IncludedSpsTradeLineItem
            .Where(x => x.NetWeightMeasure is not null)
            .SelectMany(x => ValidateNetWeight(x.SequenceNumeric.Value, x.NetWeightMeasure!.Value))
            .ToList()
            .ForEach(validationErrors.Add);
    }

    private static List<ValidationError> ValidateNetWeight(int sequenceNumeric, double netWeight)
    {
        var validationErrors = new List<ValidationError>();

        if (netWeight < MinWeight)
        {
            validationErrors.Add(new ValidationError(RuleErrorMessage.NetWeightLessThanMinWeight, RuleErrorId.NetWeightLessThanMinWeight, sequenceNumeric));
        }

        var netWeightDecimal = (decimal)netWeight;

        if (netWeightDecimal.RemoveTrailingZeros().Scale > MaxScale)
        {
            validationErrors.Add(new ValidationError(RuleErrorMessage.NetWeightTooManyDecimals, RuleErrorId.NetWeightTooManyDecimals, sequenceNumeric));
        }

        if (netWeightDecimal.Precision() > MaxPrecision)
        {
            validationErrors.Add(new ValidationError(RuleErrorMessage.NetWeightTooManyDigits, RuleErrorId.NetWeightTooManyDigits, sequenceNumeric));
        }

        return validationErrors;
    }
}
