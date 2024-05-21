namespace STW.ProcessingApi.Function.Constants;

public static class RuleErrorId
{
    public const int SystemIdMissing = 1;
    public const int ScientificNameOrEppoCodeMissing = 4;
    public const int ClassMissing = 7;
    public const int VarietyMissing = 8;
    public const int VarietyEmpty = 10;
    public const int ClassEmpty = 11;
    public const int QuantityMustBeOneOrMore = 22;
    public const int InvalidQuantityType = 26;
    public const int MissingQuantityType = 27;
    public const int NetWeightLessThanMinWeight = 29;
    public const int SequenceNumericOrder = 41;
    public const int NetWeightTooManyDecimals = 53;
    public const int NetWeightTooManyDigits = 54;
}
